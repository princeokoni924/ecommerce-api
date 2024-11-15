using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Params
{
    public class ProductSpecParams
    {
        /* definding pagination property,
         restrick the amount of data user query from data base*/
         private const int MaxPageSize = 50;
         public int PageIndex {get; set;}=1;

         private int _pageSize = 6;
         public int PageSize
         {
            get => _pageSize;
            
           set => _pageSize =(value > MaxPageSize)? MaxPageSize:value;
         }
         


        private  List<string> _brands = [];
        public List<string> Brands
        {
            get=> _brands; // here, the query string will be types = boards, glove
            set{
                _brands = value.SelectMany(splitBrands=>splitBrands.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private List<string> _types = [];
        public List<string> Types
        {
            get=> _types;
            set{
                _types = value.SelectMany(splitType=>splitType.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        
        } // searc property and backing

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set
            {
                _search = value.ToLower();
            }
        }

    

        // definding sort property
        public string? Sort { get; set; }

        

        
    }
}