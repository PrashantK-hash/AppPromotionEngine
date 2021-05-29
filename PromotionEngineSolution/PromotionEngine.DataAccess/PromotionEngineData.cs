using Newtonsoft.Json;
using PromotionEngine.Common;
using PromotionEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.DataAccess
{
    public class PromotionEngineData
    {
        public List<PromotionDetails> GetPromotionDetails()
        {
            var appDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appDirectory, Constant.PromotionDetailsFilePath);
            return JsonConvert.DeserializeObject<List<PromotionDetails>>(File.ReadAllText(filePath));
        }

        public List<SKU> GetSKUDetails()
        {
            var appDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(appDirectory, Constant.AvailableSKUFilePath);
            return JsonConvert.DeserializeObject<List<SKU>>(File.ReadAllText(filePath));
        }

    }
}
