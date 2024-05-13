using API_DataBarang;
using API_DataBarang.Model;
using API_DataBarang.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Barang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController : Controller
    {
        private static List<Barang> DataDisplayBarang = new List<Barang>();
        private string jsonFilePath;

        public BarangController()
        {
            string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\WebApplication1\\Data\\DataBarang.json";
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            DataDisplayBarang = JsonConvert.DeserializeObject<List<Barang>>(jsonData);
        }

        public static List<Barang> getDataDisplayBook()
        {
            return DataDisplayBarang;
        }

        // GET DATA ALL API
        [HttpGet]
        public IEnumerable<Barang> GET()
        {
            return DataDisplayBarang;
        }

        // GET Barang BY NamaBarang
        [HttpGet("{NamaBarang}")]
        public ActionResult<Barang> GET(string NamaBarang)
        {
            try
            {
                int id = -1;

                for (int i = 0; i < DataDisplayBarang.Count; i++)
                {
                    if (NamaBarang == DataDisplayBarang[i].NamaBarang)
                    {
                        id = i;
                    }
                }

                if (id != -1)
                {
                    return DataDisplayBarang[id];
                }
                else
                {
                    return NotFound("Nama Barang tidak ditemukan!"); // Mengembalikan respons 404 Not Found dengan pesan
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Terjadi kesalahan: " + ex.Message); // Mengembalikan respons 500 Internal Server Error dengan pesan
            }
        }

        // POST DATA Barang
        [HttpPost]
        public void POST([FromBody] Barang newBarang)
        {
            int id = -1;
            Boolean sama = false;

            // CHECK KODE Barang
            for (int i = 0; i < DataDisplayBarang.Count; i++)
            {
                if (newBarang.KodeBarang == DataDisplayBarang[i].KodeBarang)
                {
                    sama = true;
                }
            }

            if (!sama)
            {
                DataDisplayBarang.Add(newBarang);
                string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\WebApplication1\\Data\\DataBarang.json";
                string jsonContent = JsonConvert.SerializeObject(DataDisplayBarang);
                System.IO.File.WriteAllText(jsonFilePath, jsonContent);
            }
        }

        // DELETE Barang BY NAMABARANG
        [HttpDelete("NamaBarang/{NamaBarang}")]

        public void DELETEBYNAMABARANG(String NamaBarang)
        {
            int id = -1;

            for (int i = 0; i < DataDisplayBarang.Count; i++)
            {
                if (NamaBarang == DataDisplayBarang[i].NamaBarang)
                {
                    DataDisplayBarang.RemoveAt(i);
                    string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\WebApplication1\\Data\\DataBarang.json";
                    string jsonContent = JsonConvert.SerializeObject(DataDisplayBarang);
                    System.IO.File.WriteAllText(jsonFilePath, jsonContent);
                    break;
                }
            }
        }

        // DELETE Barang BY KODEBARANG
        [HttpDelete("kode/{KodeBarang}")]

        public void DELETEBYKODEBARANG(int KodeBarang)
        {
            int id = -1;

            for (int i = 0; i < DataDisplayBarang.Count; i++)
            {
                if (KodeBarang == DataDisplayBarang[i].KodeBarang)
                {
                    DataDisplayBarang.RemoveAt(i);
                    string jsonFilePath = "D:\\TugasBesar_KPL\\API_Barang\\Data\\DataBarang.json";
                    string jsonContent = JsonConvert.SerializeObject(DataDisplayBarang);
                    System.IO.File.WriteAllText(jsonFilePath, jsonContent);
                    break;
                }
            }
        }
    }
}
