namespace EasyGet
{
    public abstract class VersionFormat
    {
        public abstract Version Parse(string argument);
    }
}