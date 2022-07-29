namespace Project
{
    public class PrefsHandlerString : PrefsHandlerAbstract<string>
    {
        protected override string GetPrefs(Prefs prefs)
        {
            return prefs.GetString();
        }

        protected override void SetPrefs(Prefs prefs, string value)
        {
            prefs.Set(value);
        }
    }
}
