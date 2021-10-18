namespace ArrayFactory.Core
{
    public interface ArrayFactory<T>
    {
        /// <summary>
        /// Generates a random array with a specific length
        /// </summary>
        /// <param name="arrayLength">Length of the generated array</param>
        /// <returns></returns>
        T[] Generate(int arrayLength);
    }
}
