namespace Web.Response;

public record TaskResponse
(
    Guid Id,
    string Name,
    string Description,
    bool Finished
);