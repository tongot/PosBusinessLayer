using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class StockTrackApp : IStockTrack
    {
        private ApplicationContext _context = new ApplicationContext();

        public void AddStockTrack(StockTrack track)
        {
            _context.stockTracks.Add(track);
            _context.SaveChanges();
        }

        /// <summary>
        /// get all tracks
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StockTrack> GetAllStockTrack()
        {
            var tracks = _context.stockTracks;
            return tracks;
        }

        public StockTrack getLastStockOnProduct(int productId, DateTime date_last_updated)
        {
            var track = _context.stockTracks
                .Where(x => x.ProductId == productId).Last();
            return track;
        }

        /// <summary>
        /// get tracks for branch
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public IEnumerable<StockTrack> GetStockTrackForBranch(int BranchId)
        {
            var tracks = _context.stockTracks.Where(x => x.BranchId == BranchId);
            return tracks;
        }
        /// <summary>
        /// get all product tracks
        /// </summary>
        /// <param name="ProductId">product id</param>
        /// <returns></returns>
        public IEnumerable<StockTrack> GetStockTrackForProduct(int ProductId)
        {
            var tracks = _context.stockTracks.Where(x => x.ProductId == ProductId);
            return tracks;
        }
        /// <summary>
        /// get all the changes that where done by a user
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns></returns>
        public IEnumerable<StockTrack> GetStockTrackForUser(string username)
        {
            var tracks = _context.stockTracks.Where(x => x.EditedBy == username);
            return tracks;
        }
        /// <summary>
        /// get all tracks according to the filter values
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<StockTrack> SearchTrack(StockTrackFilter filter)
        {
            var tracks = _context.stockTracks.Where(x => x.EditedBy == filter.EditedBy || filter.EditedBy == null)
                .Where(x => x.ProductId == filter.ProductId || filter.ProductId == null)
                .Where(x => x.BranchId == filter.BranchId || filter.BranchId == null)
                .Where(x => x.IsPriceChanged || filter.IsPriceChanged == null)
                .Where(x => x.IsQuantintyChanged || filter.IsQuantityChanged == null)
                .Where(x => x.EditedOn >= filter.startDate || filter.startDate == null)
                .Where(x => x.EditedOn <= filter.endDate || filter.endDate == null)
                .Where(x => x.PriceChange >= filter.PriceChangeMin || filter.PriceChangeMin == null)
                .Where(x => x.PriceChange <= filter.PriceChangeMin || filter.PriceChangeMin == null)
                .Where(x => x.QuantityChange >= filter.QuantityChangeMin || filter.PriceChangeMin == null)
                .Where(x => x.QuantityChange <= filter.QuantityChangeMin || filter.QuantityChangeMin == null);

            return tracks;
        }
        public void updateStockTrack(StockTrack track)
        {
            _context.Set<StockTrack>().AddOrUpdate(track);
            _context.SaveChanges();
        }

    }
}
