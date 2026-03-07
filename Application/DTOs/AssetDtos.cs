namespace Application.DTOs;
// What the API returns to the client
public sealed record AssetResponse(
    Guid Id,
    string Name,
    decimal Value,
    Guid CategoryId,
    DateTime AcquiredAt);

// What the client sends to the API to create an asset
public sealed record CreateAssetRequest(
    string Name,
    decimal Value,
    Guid CategoryId,
    DateTime AcquiredAt);

// What the client sends to update an asset
public sealed record UpdateAssetRequest(
    decimal Value);