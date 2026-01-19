using AutoHub.Application.Models.Customer;
using AutoHub.Application.Models.Favorite;
using AutoHub.Application.Models.Listing;
using AutoHub.Application.Models.Seller;
using AutoHub.Application.Models.Transaction;
using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;
using AutoMapper;

namespace AutoHub.Application.Services.Mapping;

public class ApplicationProfile:Profile
{ 
    public ApplicationProfile()
    {
        CreateMap<Money, decimal>().ConvertUsing(src => src.Value);
        CreateMap<Title, string>().ConvertUsing(src => src.Value);
        CreateMap<Brand, string>().ConvertUsing(src => src.Value);
        CreateMap<Color, string>().ConvertUsing(src => src.Value);
        CreateMap<Username, string>().ConvertUsing(src => src.Value);
        CreateMap<EngineVolume, decimal>().ConvertUsing(src => src.Value);
        CreateMap<Horsepower, int>().ConvertUsing(src => src.Value);
        CreateMap<Torque, int>().ConvertUsing(src => src.Value);
        
        CreateMap<Listing, ListingModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Value))
                .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.EngineVolume.Value))
                .ForMember(dest => dest.Horsepower, opt => opt.MapFrom(src => src.Horsepower.Value))
                .ForMember(dest => dest.Torque, opt => opt.MapFrom(src => src.Torque.Value))
                .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.FuelType.ToString()))
                .ForMember(dest => dest.Aspiration, opt => opt.MapFrom(src => src.Aspiration.ToString()))
                .ForMember(dest => dest.EngineConfiguration, opt => opt.MapFrom(src => src.EngineConfiguration.ToString()))
                .ForMember(dest => dest.EngineLayout, opt => opt.MapFrom(src => src.EngineLayout.ToString()))
                .ForMember(dest => dest.TransmissionType, opt => opt.MapFrom(src => src.TransmissionType.ToString()))
                .ForMember(dest => dest.TypeOfDrive, opt => opt.MapFrom(src => src.TypeOfDrive.ToString()))
                .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.BodyType.ToString()))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer != null ? src.Buyer.Id : (Guid?)null));

            CreateMap<Seller, SellerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.ActiveListings, opt => opt.MapFrom(src => src.ActiveListings));

            CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.ObservedListings, opt => opt.MapFrom(src => src.ActiveObservedListings));

            CreateMap<Favorite, FavoriteModel>()
                .ForMember(dest => dest.ListingId, opt => opt.MapFrom(src => src.Listing.Id));

            CreateMap<Transaction, TransactionModel>()
                .ForMember(dest => dest.ListingId, opt => opt.MapFrom(src => src.Listing.Id))
                .ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.Amount.Value))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.Id))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));
        
    }
}