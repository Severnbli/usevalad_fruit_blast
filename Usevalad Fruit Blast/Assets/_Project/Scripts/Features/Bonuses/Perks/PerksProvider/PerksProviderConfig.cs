using _Project.Scripts.Features.Bonuses.Perks.Bombs;
using _Project.Scripts.Features.Bonuses.Perks.Experience;
using _Project.Scripts.Features.Bonuses.Perks.Time;
using _Project.Scripts.Features.Bonuses.Perks.Turns;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.PerksProvider
{
    [CreateAssetMenu(fileName = "PerksProviderConfig", menuName = "Configs/Bonuses/Perks/Perks Provider Config")]
    public class PerksProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private GameObject _perkPrefab;
        [SerializeField] private VolumeBombPerk[] _volumeBombPerks;
        [SerializeField] private VerticalBombPerk[] _verticalBombPerks;
        [SerializeField] private HorizontalBombPerk[] _horizontalBombPerks;
        [SerializeField] private LuckyGuyPerk[] _luckyGuyPerks;
        [SerializeField] private FastFingerPerk[] _fastFingerPerks;
        [SerializeField] private ToughNutPerk[] _toughNutPerks;
        
        public GameObject PerkPrefab => _perkPrefab;
        public VolumeBombPerk[] VolumeBombPerks => _volumeBombPerks;
        public VerticalBombPerk[] VerticalBombPerks => _verticalBombPerks;
        public HorizontalBombPerk[] HorizontalBombPerks => _horizontalBombPerks;
        public LuckyGuyPerk[] LuckyGuyPerks => _luckyGuyPerks;
        public FastFingerPerk[] FastFingerPerks => _fastFingerPerks;
        public ToughNutPerk[] ToughNutPerks => _toughNutPerks;
    }
}