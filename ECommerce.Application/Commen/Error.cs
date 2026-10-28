namespace ECommerce.Application.Commen
{
    public record Error(string Code ,string Description, ErrorType ErrorType= ErrorType.Failure)
    {
        public static Error Failure(string code= "General Failure", string description = "Genral Failure Occured")
            => new Error(code, description, ErrorType.Failure);


        public static Error Validation(string code = "General.validation ", string description = "Validation Error Occured")
            => new(code, description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound", string description = "Not Found Error Occured")
            => new(code, description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict", string description = "Conflict Error Occured")
            => new(code, description, ErrorType.Conflict);


        public static Error Forbidden(string code = "General.Forbidden", string description = "Forbidden Error Occured")
         => new(code, description, ErrorType.Forbidden);


        public static Error Invalidationis(string code = "General.Forbidden", string description = "Invalidationis Error Occured")
        => new(code, description, ErrorType.Invalidationis);

        public static Error Unauthorized(string code = "General.Forbidden", string description = "Unauthorized Error Occured")
       => new(code, description, ErrorType.Unauthorized);
    }

    public enum ErrorType
    {
        Failure=0,
        Validation,
        NotFound,
        Conflict,
        Forbidden,
        Invalidationis,
        Unauthorized,
    }
}