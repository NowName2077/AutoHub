using AutoMapper;
using AutoHub.Application.Models.Customer;
using AutoHub.Application.Models.Favorite;
using AutoHub.Application.Models.Seller;
using AutoHub.Application.Models.Listing;
using AutoHub.Application.Models.Transaction;
using AutoHub.WebHost.Requests.Customer;
using AutoHub.WebHost.Requests.Favorite;
using AutoHub.WebHost.Requests.Listing;
using AutoHub.WebHost.Requests.Seller;
using AutoHub.WebHost.Requests.Transaction;
using AutoHub.WebHost.Responses.Customer;
using AutoHub.WebHost.Responses.Favorite;
using AutoHub.WebHost.Responses.Listing;
using AutoHub.WebHost.Responses.Seller;
using AutoHub.WebHost.Responses.Transaction;

namespace AutoHub.WebHost.Mapping;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        CreateMap<CreateSellerRequest, CreateSellerModel>();
        CreateMap<CreateCustomerRequest, CreateCustomerModel>();
        CreateMap<CreateListingRequest, CreateListingModel>();
        CreateMap<CreateTransactionRequest, CreateTransactionModel>();
        CreateMap<CreateFavoriteRequest, CreateFavoriteModel>();
        
        CreateMap<SellerModel, SellerShortResponse>();
        CreateMap<SellerModel, SellerDetailedResponse>();

        CreateMap<CustomerModel, CustomerShortResponse>();
        CreateMap<CustomerModel, CustomerDetailedResponse>();

        CreateMap<ListingModel, ListingShortResponse>();
        CreateMap<ListingModel, ListingDetailedResponse>();
        
        CreateMap<TransactionModel, TransactionResponse>();
        CreateMap<FavoriteModel, FavoriteResponse>();
    }
}