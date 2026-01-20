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
        CreateMap<SellerModel, SellerShortResponse>();
        CreateMap<SellerModel, SellerDetailedResponse>();
        CreateMap<CreateSellerRequest, CreateSellerModel>();
        CreateMap<CreateSellerModel, SellerShortResponse>();
        
        CreateMap<CustomerModel, CustomerShortResponse>();
        CreateMap<CustomerModel, CustomerDetailedResponse>();
        CreateMap<CreateCustomerRequest, CreateCustomerModel>();
        CreateMap<CreateCustomerModel, CustomerShortResponse>();
        
        CreateMap<ListingModel, ListingShortResponse>();
        CreateMap<ListingModel, ListingDetailedResponse>();
        CreateMap<CreateListingRequest, CreateListingModel>();
        CreateMap<CreateListingModel, ListingShortResponse>();
        
        CreateMap<TransactionModel, TransactionResponse>();
        CreateMap<CreateTransactionRequest, CreateTransactionModel>();
        CreateMap<CreateTransactionModel, TransactionResponse>();
        
        CreateMap<FavoriteModel, FavoriteResponse>();
        CreateMap<CreateFavoriteRequest, CreateFavoriteModel>();
        CreateMap<CreateFavoriteModel, FavoriteResponse>();
    }
}