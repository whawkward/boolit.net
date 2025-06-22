using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Globalization;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System
{


    internal readonly struct Index : IEquatable<Index>
    {
        private readonly int _value;
        /// <summary>
        /// Initializes a new Index representing a position in a collection, optionally counting from the end.
        /// </summary>
        /// <param name="value">The index value, which must be non-negative.</param>
        /// <param name="fromEnd">If true, the index is counted from the end of the collection; otherwise, from the start.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }

            if (fromEnd)
                _value = ~value;
            else
                _value = value;
        }

        /// <summary>
        /// Initializes a new Index instance with the specified internal value.
        /// </summary>
        /// <param name="value">The internal representation of the index.</param>
        private Index(int value)
        {
            _value = value;
        }

        /// <summary>Create an Index pointing at first element.</summary>
        public static Index Start => new(0);

        /// <summary>Create an Index pointing at beyond last element.</summary>
        public static Index End => new(~0);

        /// <summary>Create an Index from the start at the position indicated by the value.</summary>
        /// <summary>
        /// Creates an <see cref="Index"/> representing a position counted from the start of a collection.
        /// </summary>
        /// <param name="value">The zero-based index from the start. Must be non-negative.</param>
        /// <returns>An <see cref="Index"/> instance representing the specified position from the start.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromStart(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }

            return new Index(value);
        }

        /// <summary>Create an Index from the end at the position indicated by the value.</summary>
        /// <summary>
        /// Creates an <see cref="Index"/> representing a position counted from the end of a collection.
        /// </summary>
        /// <param name="value">The zero-based index from the end. Must be non-negative.</param>
        /// <returns>An <see cref="Index"/> instance representing the specified position from the end.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromEnd(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }

            return new Index(~value);
        }

        /// <summary>Returns the index value.</summary>
        public int Value
        {
            get
            {
                if (_value < 0)
                {
                    return ~_value;
                }
                else
                {
                    return _value;
                }
            }
        }

        /// <summary>Indicates whether the index is from the start or the end.</summary>
        public bool IsFromEnd => _value < 0;

        /// <summary>Calculate the offset from the start using the giving collection length.</summary>
        /// <param name="length">The length of the collection that the Index will be used with. length has to be a positive value</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter and the returned offset value against negative values.
        /// we don't validate either the returned offset is greater than the input length.
        /// It is expected Index will be used with collections which always have non negative length/count. If the returned offset is negative and
        /// then used to index a collection will get out of range exception which will be same affect as the validation.
        /// <summary>
        /// Calculates the zero-based offset represented by this index for a collection of the specified length.
        /// </summary>
        /// <param name="length">The total number of elements in the collection.</param>
        /// <returns>The zero-based offset corresponding to this index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            var offset = _value;
            if (IsFromEnd)
            {
                // offset = length - (~value)
                // offset = length + (~(~value) + 1)
                // offset = length + value + 1

                offset += length + 1;
            }
            return offset;
        }

        /// <summary>Indicates whether the current Index object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
#pragma warning disable S927 // Parameter names should match base declaration and other partial definitions
        /// <summary>
/// Determines whether the specified object is an <see cref="Index"/> and has the same value as the current instance.
/// </summary>
/// <param name="value">The object to compare with the current <see cref="Index"/>.</param>
/// <returns><c>true</c> if the specified object is an <see cref="Index"/> and has the same value; otherwise, <c>false</c>.</returns>
public override bool Equals(object? value) => value is Index index && _value == index._value;
#pragma warning restore S927 // Parameter names should match base declaration and other partial definitions

        /// <summary>Indicates whether the current Index object is equal to another Index object.</summary>
        /// <summary>
/// Determines whether this <see cref="Index"/> instance is equal to another <see cref="Index"/>.
/// </summary>
/// <param name="other">The <see cref="Index"/> to compare with this instance.</param>
/// <returns><c>true</c> if the two <see cref="Index"/> instances represent the same position; otherwise, <c>false</c>.</returns>
        public bool Equals(Index other) => _value == other._value;

        /// <summary>
/// Returns the hash code for this Index instance.
/// </summary>
/// <returns>The hash code representing the internal value of the Index.</returns>
        public override int GetHashCode() => _value;

        /// <summary>Converts integer number to an Index.</summary>
        public static implicit operator Index(int value) => FromStart(value);

        /// <summary>
                /// Returns a string representation of the Index, prefixing with '^' if it is from the end.
                /// </summary>
                /// <returns>A string representing the index, with a '^' prefix if the index is from the end.</returns>
        public override string ToString()
            => IsFromEnd
                ? $"^{((uint)Value).ToString(CultureInfo.InvariantCulture)}"
                : ((uint)Value).ToString(CultureInfo.InvariantCulture);        
    }

    /// <summary>Construct a Range object using the start and end indexes.</summary>
    internal readonly struct Range : IEquatable<Range>
    {
        /// <param name="start">Represent the inclusive start index of the range.</param>
        /// <param name="end">Represent the exclusive end index of the range.</param>
#pragma warning disable IDE0290 // Use primary constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct with the specified inclusive start and exclusive end indices.
        /// </summary>
        /// <param name="start">The inclusive start index of the range.</param>
        /// <param name="end">The exclusive end index of the range.</param>
        public Range(Index start, Index end)
#pragma warning restore IDE0290 // Use primary constructor
        {
            Start = start;
            End = end;
        }

        /// <summary>Represent the inclusive start index of the Range.</summary>
        public Index Start { get; }

        /// <summary>Represent the exclusive end index of the Range.</summary>
        public Index End { get; }

        /// <summary>Indicates whether the current Range object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
#pragma warning disable S927 // Parameter names should match base declaration and other partial definitions
        /// <summary>
            /// Determines whether the specified object is a <see cref="Range"/> and has the same start and end indices as this instance.
            /// </summary>
            /// <param name="value">The object to compare with the current <see cref="Range"/>.</param>
            /// <returns><c>true</c> if the specified object is a <see cref="Range"/> with equal start and end indices; otherwise, <c>false</c>.</returns>
            public override bool Equals(object? value) =>
            value is Range r &&
            r.Start.Equals(Start) &&
            r.End.Equals(End);
#pragma warning restore S927 // Parameter names should match base declaration and other partial definitions

        /// <summary>Indicates whether the current Range object is equal to another Range object.</summary>
        /// <summary>
/// Determines whether the current range is equal to the specified range.
/// </summary>
/// <param name="other">The range to compare with the current range.</param>
/// <returns><c>true</c> if both the start and end indices are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        /// <summary>
        /// Computes a hash code for the current Range instance based on its Start and End indices.
        /// </summary>
        /// <returns>A hash code representing this Range.</returns>
        public override int GetHashCode()
        {
            return Start.GetHashCode() * 31 + End.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the range in the format "Start..End".
        /// </summary>
        public override string ToString()
        {
            return Start + ".." + End;
        }

        /// <summary>
/// Creates a range that starts at the specified index and extends to the end of a collection.
/// </summary>
/// <param name="start">The inclusive start index of the range.</param>
/// <returns>A range from the specified start index to the end.</returns>
        public static Range StartAt(Index start) => new(start, Index.End);

        /// <summary>
/// Creates a Range that starts at the beginning of a collection and ends at the specified end index.
/// </summary>
/// <param name="end">The exclusive end index of the range.</param>
/// <returns>A Range from the start of the collection to the specified end index.</returns>
        public static Range EndAt(Index end) => new(Index.Start, end);

        /// <summary>Create a Range object starting from first element to the end.</summary>
        public static Range All => new(Index.Start, Index.End);

        

        /// <summary>Calculate the start offset and length of range object using a collection length.</summary>
        /// <param name="length">The length of the collection that the range will be used with. length has to be a positive value.</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter against negative values.
        /// It is expected Range will be used with collections which always have non negative length/count.
        /// We validate the range is inside the length scope though.
        /// <summary>
        /// Calculates the start offset and length of the range within a collection of the specified length.
        /// </summary>
        /// <param name="length">The total length of the collection to which the range will be applied.</param>
        /// <returns>A tuple containing the zero-based start offset and the length of the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the computed start or end offset is outside the bounds of the collection length, or if the start is greater than the end.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start;
            var startIndex = Start;
            if (startIndex.IsFromEnd)
                start = length - startIndex.Value;
            else
                start = startIndex.Value;

            int end;
            var endIndex = End;
            if (endIndex.IsFromEnd)
                end = length - endIndex.Value;
            else
                end = endIndex.Value;

            if ((uint)end > (uint)length || (uint)start > (uint)end)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            return (start, end - start);
        }
    }
}

namespace System.Runtime.CompilerServices
{
    internal static class RuntimeHelpers
    {
        /// <summary>
        /// Slices the specified array using the specified range.
        /// <summary>
        /// Returns a new array containing the elements within the specified range of the input array.
        /// </summary>
        /// <param name="array">The source array to slice.</param>
        /// <param name="range">The range specifying the segment to extract.</param>
        /// <returns>A new array containing the elements in the specified range.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the range is outside the bounds of the array.</exception>
        public static T[] GetSubArray<T>(T[] array, Range range)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            (int offset, int length) = range.GetOffsetAndLength(array.Length);

            if (default(T) is not null || typeof(T[]) == array.GetType())
            {
                // We know the type of the array to be exactly T[].

                if (length == 0)
                {
                    return [];
                }

                var dest = new T[length];
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
            else
            {
                // The array is actually a U[] where U:T.
                var dest = (T[])Array.CreateInstance(array.GetType().GetElementType(), length);
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
        }
    }
}


#pragma warning restore IDE0130 // Namespace does not match folder structure