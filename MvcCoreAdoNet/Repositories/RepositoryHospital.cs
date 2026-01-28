using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;
using System.Data;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;
        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITALES;Persist Security Info=True;User ID=SA;Password=Admin123;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string sql = "select * from HOSPITAL";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Hospital> hospitales = new List<Hospital>();
            while(await this.reader.ReadAsync())
            {
                Hospital h = new Hospital();
                h.IdHospital = int.Parse(this.reader["Hospital_cod"].ToString());
                h.Nombre = this.reader["Nombre"].ToString();
                h.Direccion = this.reader["direccion"].ToString();
                h.Telefono = this.reader["telefono"].ToString();
                h.Camas  = int.Parse(this.reader["num_cama"].ToString());
                hospitales.Add(h);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return hospitales;
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            string sql = "select * from Hospital where hospital_cod=@hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Hospital hospital = new Hospital();
            await this.reader.ReadAsync();
            hospital.IdHospital = int.Parse(this.reader["hospital_cod"].ToString());
            hospital.Nombre = this.reader["Nombre"].ToString();
            hospital.Direccion = this.reader["direccion"].ToString();
            hospital.Telefono = this.reader["telefono"].ToString();
            hospital.Camas = int.Parse(this.reader["num_cama"].ToString());
            
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return hospital;

        }
        public async Task InsertHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            string sql = "insert into Hospital values (@hospitalcod,@nombre,@direccion,@telefono,@camas)";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            string sql = "update Hospital set Nombre=@nombre, direccion=@direccion, telefono=@telefono, num_cama=@camas where hospital_cod=@hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            string sql = "delete from hospital where hospital_cod=@hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task <List<Doctor>> GetDoctoresAsync()
        {
            string sql = "select * from doctor";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Doctor> doctores = new List<Doctor>();
            while(await this.reader.ReadAsync())
            {
                Doctor d= new Doctor();
                int hosp = int.Parse(this.reader["Hospital_cod"].ToString());
                int num = int.Parse(this.reader["doctor_no"].ToString());
                string ape = this.reader["apellido"].ToString();
                string esp = this.reader["especialidad"].ToString();
                int sal = int.Parse(this.reader["salario"].ToString());
                d.HospitalCod = hosp;
                d.DoctorNum = num;
                d.Apellido = ape;
                d.Especialidad = esp;
                d.Salario = sal;
                doctores.Add(d);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }
        public async Task<List<Doctor>> FindEspecialidadAsync (string especialidad)
        {
            string sql = "select * from doctor where especialidad= @especialidad";
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Doctor> doctores = new List<Doctor>();
            while (await this.reader.ReadAsync())
            {
                Doctor d = new Doctor();
                int hosp = int.Parse(this.reader["Hospital_cod"].ToString());
                int num = int.Parse(this.reader["doctor_no"].ToString());
                string ape = this.reader["apellido"].ToString();
                string esp = this.reader["especialidad"].ToString();
                int sal = int.Parse(this.reader["salario"].ToString());
                d.HospitalCod = hosp;
                d.DoctorNum = num;
                d.Apellido = ape;
                d.Especialidad = esp;
                d.Salario = sal;
                doctores.Add(d);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return doctores;
        }
    }
}
