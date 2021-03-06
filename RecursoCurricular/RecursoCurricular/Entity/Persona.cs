using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace RecursoCurricular
{
    [Serializable]
    public partial class Persona : IEquatable<Persona>
    {
        public Persona()
        {
            this.Id = Guid.NewGuid();
            this.comuna = default(EntityRef<RecursoCurricular.Comuna>);
            this.estadoCivil = default(EntityRef<RecursoCurricular.EstadoCivil>);
            this.nacionalidad = default(EntityRef<RecursoCurricular.Nacionalidad>);
            this.nivelEducacional = default(EntityRef<RecursoCurricular.NivelEducacional>);
            this.sexo = default(EntityRef<RecursoCurricular.Sexo>);
        }

        public Guid Id { get; set; }

        [Display(Name = "R.U.N.:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el R.U.N.")]
        public String Run { get; set; }

        public Int32 RunCuerpo { get; set; }

        public Char RunDigito { get; set; }

        public String Nombre { get; set; }

        [Display(Name = "Nombre:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre")]
        public String Nombres { get; set; }

        [Display(Name = "Apellido paterno:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el apellido paterno")]
        public String ApellidoPaterno { get; set; }

        [Display(Name = "Apellido materno:")]
        public String ApellidoMaterno { get; set; }

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Mail incorrecto")]
        public String Email { get; set; }

        [Display(Name = "Sexo:")]
        [Range(1, 2, ErrorMessage = "Debe seleccionar el sexo")]
        public Int16 SexoCodigo { get; set; }

        [Display(Name = "Fecha de nacimiento:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [Range(typeof(DateTime), "01/01/1900", "31/12/2100", ErrorMessage = "La fecha de nacimiento es errónea")]
        public Nullable<DateTime> FechaNacimiento { get; set; }

        [Display(Name = "Nacionalidad:")]
        public Nullable<Int16> NacionalidadCodigo { get; set; }

        [Display(Name = "Estado civil:")]
        public Nullable<Int16> EstadoCivilCodigo { get; set; }

        [Display(Name = "Nivel educacional:")]
        public Nullable<Int32> NivelEducacionalCodigo { get; set; }

        [Display(Name = "Región:")]
        public Nullable<Int16> RegionCodigo { get; set; }

        [Display(Name = "Ciudad:")]
        public Nullable<Int16> CiudadCodigo { get; set; }

        [Display(Name = "Comuna:")]
        public Nullable<Int16> ComunaCodigo { get; set; }

        [Display(Name = "Villa o población:")]
        public String VillaPoblacion { get; set; }

        [Display(Name = "Dirección:")]
        public String Direccion { get; set; }

        [Display(Name = "Teléfono:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número de teléfono es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número de teléfono es erróneo")]
        public Nullable<Int32> Telefono { get; set; }

        [Display(Name = "Teléfono celular:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número de celular es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número de celular es erróneo")]
        public Nullable<Int32> Celular { get; set; }

        [Display(Name = "Observaciones:")]
        public String Observaciones { get; set; }

        [Display(Name = "Ocupación:")]
        public String Ocupacion { get; set; }

        [Display(Name = "Teléfono laboral:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número del teléfono laboral es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número del teléfono laboral es erróneo")]
        public Nullable<Int32> TelefonoLaboral { get; set; }

        [Display(Name = "Dirección laboral:")]
        public String DireccionLaboral { get; set; }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Comuna> comuna;
        public RecursoCurricular.Comuna Comuna
        {
            get { return this.comuna.Entity; }
            set { this.comuna.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.EstadoCivil> estadoCivil;
        public RecursoCurricular.EstadoCivil EstadoCivil
        {
            get { return this.estadoCivil.Entity; }
            set { this.estadoCivil.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Nacionalidad> nacionalidad;
        public RecursoCurricular.Nacionalidad Nacionalidad
        {
            get { return this.nacionalidad.Entity; }
            set { this.nacionalidad.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.NivelEducacional> nivelEducacional;
        public RecursoCurricular.NivelEducacional NivelEducacional
        {
            get { return this.nivelEducacional.Entity; }
            set { this.nivelEducacional.Entity = value; }
        }

        [NonSerialized]
        private EntityRef<RecursoCurricular.Sexo> sexo;
        public RecursoCurricular.Sexo Sexo
        {
            get { return this.sexo.Entity; }
            set { this.sexo.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.Personas.Attach(this);
        }

        public bool Equals(Persona other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Persona)) return false;
            return Equals((Persona)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}