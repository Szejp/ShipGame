using System.Linq;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.GameModule.GameTools.Teams
{
    public static class Teams
    {
        static TeamsConfig teamsConfig;

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            teamsConfig = Configs.GetConfig<TeamsConfig>();
        }

        public static bool AreInTheSameTeam(int teamId, int otherTeamId)
        {
            return teamsConfig.teams.Exists(p => p.Split(',').Contains(teamId.ToString()) && p.Contains(otherTeamId.ToString()));
        }
    }
}