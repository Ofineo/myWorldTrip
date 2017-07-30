using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWordlRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUsername(string username);
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        void AddStop(string tripName, Stop newStop, string username);
        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string username);
   
       
    }
}