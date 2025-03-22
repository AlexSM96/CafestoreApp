namespace Cafestore.Domain.Exceptions;

public class AlreadyExistsException(string message) : Exception(message);
