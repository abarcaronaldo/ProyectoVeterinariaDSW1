using NuGet.Protocol.Core.Types;
using ProyectoVeterinaria_DSW1.Constants;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class CitaService
    {
        IAgenda _agenda;
        ICita _cita;

        public CitaService(IAgenda agenda, ICita cita)
        {
            _agenda = agenda;
            _cita = cita;
        }

        public IEnumerable<AgendaDisponibilidadViewModel> ConsultarDisponibilidad(DateOnly fecha)
        {
            return _agenda.BuscarDisponibilidad(fecha);
        }

        public Agenda ObtenerAgendaPorId(int idAgenda)
        {
            return _agenda.buscar(idAgenda);
        }

        public TimeSpan? ObtenerHoraSugerida(int idAgenda, DateOnly fecha, string horaDeseada, int duracion)
        {
            var hora = TimeSpan.Parse(horaDeseada); //se covierte la hora de string a timeSpan

            //cuanto tiempo a pasado desde las 00:00 medianoche
            //ejemplo hora = 10:17 -> 10 horas = 600 minutos, entonces total: 617 minutos
            var minutosTotales = (int)hora.TotalMinutes;

            //duracion igual a 30 minutos cada cita
            //ejemplo: 617 % 30 = 17, sobra 17
            //resta el residuo (17) para bajar al bloque inferior - (617-17) = 600
            //600 minutos, corresponde a 10:00 am
            var bloqueInicial = TimeSpan.FromMinutes(minutosTotales - (minutosTotales % duracion));

            //obtener horas ocupadas de ese dia de cita
            var ocupadas = _agenda.ObtenerHorasOcupadas(idAgenda, fecha);

            var horaSugerida = bloqueInicial; //10:00 am -> ejemplo

            //bucle para encontrar un bloque libre
            //como decir "la lista ocupadas, ¿contine la hora 10:00 am?
            while (ocupadas.Contains(horaSugerida))
            {
                //como esta ocupado 10:00 am, añadimos unos 30 minutos
                //ahora 10:30 am
                horaSugerida = horaSugerida.Add(TimeSpan.FromMinutes(duracion));

                //verificamos que esa hora, no sobrepase el limite de la hora fin
                if (horaSugerida >= _agenda.ObtenerHoraFin(idAgenda))
                    return null; //no quedan bloques
            }

            return horaSugerida;
        }

        public int RegistrarCita(RegistrarCitaViewModel vm, int idDueno)
        {

            Cita cita = new Cita()
            {
                IdAgenda = vm.IdAgenda,
                IdDueno = idDueno,
                IdMascota = vm.IdMascota,
                FechaCita = DateOnly.FromDateTime(vm.FechaCita),
                HoraCita = vm.HoraCita,
                Motivo = vm.Motivo
            };

            return _cita.RegistrarCita(cita); 
        }

        public IEnumerable<CitaListadoViewModel> MisCitas(int idDueno)
        {
            return _cita.MisCitas(idDueno);
        }

        public CitaListadoViewModel ObtenerCita(int idCita, int idDueno)
        {
            return _cita.BuscarCita(idCita, idDueno);
        }

        public string Actualizar(EditarCitaViewModel model, int idDueno)
        {
            if (model.IdCita <= 0)
                return "Cita inválida";

            if (model.IdMascota <= 0)
                return "Debe seleccionar una mascota";

            if (string.IsNullOrWhiteSpace(model.Motivo))
                return "El motivo no puede estar vacío";

            var cita = new Cita
            {
                IdCita = model.IdCita,
                IdMascota = model.IdMascota,
                Motivo = model.Motivo,
                IdDueno = idDueno
            };

            return _cita.actualizar(cita);
        }

    }
}
