using AutoMapper;
using AutoHub.Application.Models.Customer;
using AutoHub.Application.Models.Seller;
using AutoHub.Application.Models.Listing;
using AutoHub.WebHost.Requests;
using AutoHub.WebHost.Requests.Seller;
using AutoHub.WebHost.Responses;
using AutoHub.WebHost.Responses.Seller;

namespace AutoHub.WebHost.Mapping;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        CreateMap<CreateSellerRequest, CreateSellerModel>();
        CreateMap<CreateCustomerRequest, CreateCustomerModel>();
        CreateMap<CreateListingRequest, CreateListingModel>();

        CreateMap<SellerModel, SellerShortResponse>();
        CreateMap<SellerModel, SellerDetailedResponse>();

        CreateMap<CustomerModel, CustomerShortResponse>();
        CreateMap<CustomerModel, CustomerDetailedResponse>();

        CreateMap<ListingModel, ListingShortResponse>();
        CreateMap<ListingModel, ListingDetailedResponse>();
    }
}