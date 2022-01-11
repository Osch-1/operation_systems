namespace CSharpLexicalAnalyzer
{
    public class TokenizationInfo
    {
        private readonly TokenInfo _tokenInfo;
        public TokenInfo TokenInfo => _tokenInfo;

        private readonly string? _sault;
        public string? Sault => _sault;

        public TokenizationInfo( TokenInfo tokenInfo, string? sault )
        {
            _tokenInfo = tokenInfo;
            _sault = sault;            
        }
    }
}
