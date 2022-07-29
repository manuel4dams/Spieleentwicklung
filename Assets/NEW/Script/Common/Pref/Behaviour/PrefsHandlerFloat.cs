namespace Project
{
    public class PrefsHandlerFloat : PrefsHandlerAbstract<float>
    {
        protected override float GetPrefs(Prefs prefs)
        {
            return prefs.GetFloat();
        }

        protected override void SetPrefs(Prefs prefs, float value)
        {
            prefs.Set(value);
        }
    }
}
