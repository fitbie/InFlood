using System;

namespace InFlood.Utils
{
    /// <summary>
    /// Helper Extension for simple arrays shuffling. 
    /// </summary>
    public static class RandomShuffleArray
    {
        /// <summary>
        /// Shuffle whole array.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="array">Source array</param>
        public static void ShuffleArray<T>(this Random rnd, T[] array)
        {
            int counter = array.Length;

            while (counter > 1)
            {
                int pos = rnd.Next(counter--);
                (array[pos], array[counter]) = (array[counter], array[pos]);
            }
        }

        /// <summary>
        /// Shuffles array from start index inclusive to end exclusive.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="array">Source array</param>
        /// <param name="start">Start (usually index)</param>
        /// <param name="end">End (usually length)</param>
        /// <typeparam name="T"></typeparam>
        public static void ShuffleArray<T>(this Random rnd, T[] array, int start, int end)
        {
            while (end > start)
            {
                int pos = rnd.Next(start, end--);
                (array[pos], array[end]) = (array[end], array[pos]);
            }
        }
    }

}