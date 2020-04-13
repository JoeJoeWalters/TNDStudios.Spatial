namespace TNDStudios.Spatial.Documents
{
    public interface IGeoFileConvertable
    {
        /// <summary>
        /// Requirement to have a method that converts to the common format
        /// </summary>
        /// <returns>A common GeoFile conversion of the origional</returns>
        GeoFile ToGeoFile();
    }
}
