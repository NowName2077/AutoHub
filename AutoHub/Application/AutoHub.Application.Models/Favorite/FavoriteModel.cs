using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Favorite;

public record class FavoriteModel(Guid Id,Guid ListingId): IModel<Guid>;