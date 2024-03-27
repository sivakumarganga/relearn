namespace Relearn.DS.PriorityPageing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class Program
    {
        public static Dictionary<string, List<Offer>> offerRepo = new Dictionary<string, List<Offer>>()
        {
            { "p1",new List<Offer>()},
            { "p2",new List<Offer>()},
            { "p3",new List<Offer>()},
        };
        static void Main(string[] args)
        {
            int totalRecords = OfferRepoInit(2, 20, 50);
            string continuationToken = string.Empty;
            List<Offer> offerList = new List<Offer>();
            int pageIndex = 1, pageSize = 20,totalPages= (totalRecords / pageSize)+1;
            while (pageIndex <= totalPages)
            {
                (continuationToken, offerList) = PrepareOffers(continuationToken, pageIndex++, pageSize);
                Console.WriteLine($" Token:{continuationToken} PageIndex:{pageIndex} PageSize:{pageSize} Records Fetched:{offerList.Count}");
            }
            Console.Read();
        }
        
        public static (string,List<Offer>) PrepareOffers(string continuationToken,int pageIndex, int pageSize)
        {
            List<Offer> offers = new List<Offer>();
            int offerIndex = (pageIndex-1) * pageSize;
            if(string.IsNullOrEmpty(continuationToken))
            {
                continuationToken = $"0,-1,p1:0,-1,p2:0,-1,p3";
            }
            Dictionary<string, OfferTypeContinuation> offerQuanityValues = new Dictionary<string, OfferTypeContinuation>();
            int itemsToBeFetched = pageSize;
            int currentPageRecordCount = 0;
            int offersSkipped = 0;
            foreach (var offerTypePair in continuationToken.Split(':'))
            {
                var offerDetail = offerTypePair.Split(',');
                var offerAgg = new OfferTypeContinuation(offerDetail.Last(), int.Parse(offerDetail[0].Trim()), int.Parse(offerDetail[1].Trim()));
                if (offerAgg.Total == -1)
                {
                    //Fetch the total
                    offerAgg.Total = GetOfferTotalCount(offerAgg.OfferType);
                }

                if (offerIndex < (offersSkipped+offerAgg.Total))
                {
                    while (true)
                    {
                        var (index, itemsToSkip, totalRecordsFetched) = CalculatePageIndexAndItemsSkip(offerAgg.AddedCount, pageSize, offerAgg.Total);
                        var offerTypeList = GetOfferByOfferType(offerAgg.OfferType, index, pageSize, itemsToBeFetched, itemsToSkip);
                        if (totalRecordsFetched > itemsToBeFetched)
                        {
                            //Take only itemstobefteched
                            totalRecordsFetched = itemsToBeFetched;
                        }
                        currentPageRecordCount = currentPageRecordCount + totalRecordsFetched;
                        offerAgg.AddedCount = offerAgg.AddedCount + totalRecordsFetched;
                        itemsToBeFetched = pageSize - currentPageRecordCount;
                        //Console.WriteLine($"Result Index:{index} Items to Skip:{itemsToSkip} Current Record Fetched: {totalRecordsFetched} Total Fetched:{currentPageRecordCount}");
                        offers.AddRange(offerTypeList);
                        if (currentPageRecordCount == pageSize || (offerAgg.Total == offerAgg.AddedCount)) break;
                    }
                }
                else
                {
                    offersSkipped = offersSkipped + offerAgg.Total;
                }   
                offerQuanityValues.Add(offerDetail.Last(), offerAgg);
            }
            StringBuilder sb=new StringBuilder();
            foreach (var od in offerQuanityValues)
            {
                sb.Append($"{od.Value.AddedCount},{od.Value.Total},{od.Key}:");
            }
            return (sb.ToString().TrimEnd(':'), offers);
        }

        public static (int,int, int) CalculatePageIndexAndItemsSkip(int itemsAdded,int pageSize,int totalRecords)
        {
            int pageIndex = itemsAdded / pageSize;
            int itemsToSkip = itemsAdded % pageSize;
            int recordsFetched = Math.Min(totalRecords-(pageIndex*pageSize), pageSize)-itemsToSkip;
            return (pageIndex, itemsToSkip, recordsFetched);
        }
        public static List<Offer> GetOfferByOfferType(string offerType,int index,int pageSize,int toBeFetched,int toBeSkipped)
        {
            var currentTypeOffers = offerRepo[offerType];
            List<Offer> result = currentTypeOffers.Skip(index*pageSize).Take(pageSize).Skip(toBeSkipped).Take(toBeFetched).ToList();
            return result;
        }
        public static int OfferRepoInit(int p1Count,int p2Count,int p3Count)
        {
            int offerIndex = 0;
            for (int i = 0; i < p1Count; i++)
            {
                offerIndex++;
                Offer offer = new Offer()
                {
                    Id = offerIndex,
                    Name = $"P1:{offerIndex}"
                };
                offerRepo["p1"].Add(offer);
            }
            for (int i = 0; i < p2Count; i++)
            {
                offerIndex++;
                Offer offer = new Offer()
                {
                    Id = offerIndex,
                    Name = $"P2:{offerIndex}"
                };
                offerRepo["p2"].Add(offer);
            }
            for (int i = 0; i < p3Count; i++)
            {
                offerIndex++;
                Offer offer = new Offer()
                {
                    Id = offerIndex,
                    Name = $"P3:{offerIndex}"
                };
                offerRepo["p3"].Add(offer);
            }
            return (p1Count+p2Count+p3Count);
           
        }
        public static int GetOfferTotalCount(string offerType)
        {
            return offerRepo[offerType].Count;
        }
        public class OfferTypeContinuation
        {
            public OfferTypeContinuation(string offerType, int addedCount,int total)
            {
                 OfferType = offerType;
                AddedCount = addedCount;
                Total = total;
            }
            public string OfferType { get; set; }
            public int AddedCount { get; set; }
            public int Total { get; set; }
        }
    }
}