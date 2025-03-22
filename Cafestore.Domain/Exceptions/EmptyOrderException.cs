namespace Cafestore.Domain.Exceptions;

public class EmptyOrderException(string message) : Exception(message);