namespace Characters.Config
{
    public interface ICharactersEntity
    {
        CharacterConfig GetCharacterConfig(CharacterType characterType);
    }
}