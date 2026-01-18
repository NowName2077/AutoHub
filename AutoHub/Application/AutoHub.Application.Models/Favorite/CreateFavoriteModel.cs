using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Favorite;

public record class CreateFavoriteModel(Guid ListingId): ICreateModel;