using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Exceptions;

public class NotForeseenSituationForThisListingStatusException(Listing lot, LotStatus status)
        : InvalidOperationException($"Not foreseen situation of transaction for this listing status {status} (id = {lot.Id})")
    {
        public Listing Listing => lot;
        public LotStatus Status => status;
    }
