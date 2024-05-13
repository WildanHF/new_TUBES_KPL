using API_DataPelanggan;
using API_DataPelanggan.Model;
using API_DataPelanggan.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Barang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkunController : Controller
    {
        private static List<Akun> DataDisplayAkun = new List<Akun>();
        private string jsonFilePath;

        public AkunController()
        {
            string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\DataPelanggan\\Data\\DataAkun.json";
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            DataDisplayAkun = JsonConvert.DeserializeObject<List<Akun>>(jsonData);
        }

        public static List<Akun> getDataDisplayBook()
        {
            return DataDisplayAkun;
        }

        // GET DATA ALL API
        [HttpGet]
        public IEnumerable<Akun> GET()
        {
            return DataDisplayAkun;
        }

        // GET Akun BY Nama
        [HttpGet("{Nama}")]
        public ActionResult<Akun> GET(string Nama)
        {
            try
            {
                int id = -1;

                for (int i = 0; i < DataDisplayAkun.Count; i++)
                {
                    if (Nama == DataDisplayAkun[i].Nama)
                    {
                        id = i;
                    }
                }

                if (id != -1)
                {
                    return DataDisplayAkun[id];
                }
                else
                {
                    return NotFound("Nama tidak ditemukan!"); // Mengembalikan respons 404 Not Found dengan pesan
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Terjadi kesalahan: " + ex.Message); // Mengembalikan respons 500 Internal Server Error dengan pesan
            }
        }

        // POST DATA Akun
        [HttpPost]
        public void POST([FromBody] Akun newAkun)
        {
            Boolean sama = false;

            // CHECK Nama Barang
            for (int i = 0; i < DataDisplayAkun.Count; i++)
            {
                if (newAkun.Nama == DataDisplayAkun[i].Nama)
                {
                    sama = true;
                }
            }

            if (!sama)
            {
                DataDisplayAkun.Add(newAkun);
                string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\DataPelanggan\\Data\\DataAkun.json";
                string jsonContent = JsonConvert.SerializeObject(DataDisplayAkun);
                System.IO.File.WriteAllText(jsonFilePath, jsonContent);
            }
        }

        // DELETE Akun BY NAMA
        [HttpDelete("Nama/{Nama}")]

        public void DELETEBYNAMA(String Nama)
        {
            int id = -1;

            for (int i = 0; i < DataDisplayAkun.Count; i++)
            {
                if (Nama == DataDisplayAkun[i].Nama)
                {
                    DataDisplayAkun.RemoveAt(i);
                    string jsonFilePath = "D:\\SEMESTER 4\\PRAK KPL\\Tubes\\Solution1\\DataPelanggan\\Data\\DataAkun.json";
                    string jsonContent = JsonConvert.SerializeObject(DataDisplayAkun);
                    System.IO.File.WriteAllText(jsonFilePath, jsonContent);
                    break;
                }
            }
        }
    }
}