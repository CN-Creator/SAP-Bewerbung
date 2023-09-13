using API.Data.Models;

namespace API.Interfaces
{
    public interface IAktorRepository
    {
        void Update(Aktor aktor);

        Task<bool> DoesAktorWithIdExist(string aktorName);

        Task<IEnumerable<Aktor>> GetAktorsAsync(int currentPage, int maxPerPage);

        Task<Aktor> GetAktorByIdAsync(string aktorId);

        void AddAktorToDatabase(Aktor aktor);
    }
}