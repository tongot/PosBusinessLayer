using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public interface IStockTrack
    {
        void AddStockTrack(StockTrack track);
        IEnumerable<StockTrack> GetAllStockTrack();
        IEnumerable<StockTrack> GetStockTrackForBranch(int BranchId);
        IEnumerable<StockTrack> GetStockTrackForProduct(int Product);
        IEnumerable<StockTrack> GetStockTrackForUser(string username);
        IEnumerable<StockTrack> SearchTrack(StockTrackFilter filter);
        StockTrack getLastStockOnProduct(int productId, DateTime date_last_updated);
        void updateStockTrack(StockTrack track);

    }
}
