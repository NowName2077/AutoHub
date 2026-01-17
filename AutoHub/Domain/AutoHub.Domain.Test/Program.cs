using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Test;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== AutoHub domain smoke test ===");

            // Create users
            var seller = new Seller(Guid.NewGuid(), new Username("seller_1"));
            var buyer = new Customer(Guid.NewGuid(), new Username("buyer_1"));

            Console.WriteLine($"Seller created: {seller.Username}");
            Console.WriteLine($"Buyer created: {buyer.Username}");

            // Create a car
            var brand = new Brand("Toyota");
            var engine = new EngineVolume(2.0m);
            var hp = new Horsepower(150);
            var torque = new Torque(200);
            var color = new Color("Red");

            var car = seller.CreateCar(brand, engine, hp, torque, FuelType.Petrol, Aspiration.NaturallyAspirated,
                EngineConfiguration.Inline, EngineLayout.Front, TypeOfDrive.FWD, TransmissionType.Manual,
                BodyType.Sedan, color);

            Console.WriteLine($"Car created for seller: {brand} {car.Id}");

            // Create listing
            var title = new Title("Nice Toyota");
            var price = new Money(100000m);
            var listing = seller.CreateListing(title, car, price, DateTime.UtcNow);

            Console.WriteLine($"Listing created: {listing.Title}, price: {listing.Price}");

            // Buyer tries to purchase with insufficient funds
            try
            {
                var offered = new Money(50000m);
                Console.WriteLine($"Buyer tries to buy with {offered}");
                var tx = buyer.MakeTransaction(listing, offered);
                Console.WriteLine("Transaction unexpectedly succeeded: " + tx.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expected failure on insufficient funds: " + ex.Message);
            }

            // Buyer supplies full price
            var offeredFull = new Money(100000m);
            var transaction = buyer.MakeTransaction(listing, offeredFull);
            Console.WriteLine($"Transaction succeeded: amount {transaction.Amount}, listing {transaction.Listing.Id}");

            // Try to cancel completed listing as seller (should throw)
            try
            {
                seller.CancelListing(listing);
                Console.WriteLine("Cancel succeeded unexpectedly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expected failure on cancel completed listing: " + ex.Message);
            }

            Console.WriteLine("=== Test finished ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error during smoke test: " + ex);
        }
    }
}