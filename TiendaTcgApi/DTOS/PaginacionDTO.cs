namespace TiendaTcgApi.DTOS
{
    public record PaginacionDTO(int pagina =1, int recordPorPaginas = 10)
    {
        private const int CantidadMaximaRegistrosPorPagina = 50;

        public int Pagina { get; init; } = Math.Max(1,pagina);

        public int RecordPorPaginas { get; init; } = Math.Clamp(recordPorPaginas, 1, CantidadMaximaRegistrosPorPagina);
        public int RecordsPorPagina { get; internal set; }
    }
}
