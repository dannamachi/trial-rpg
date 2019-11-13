using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public static class GameCheck
    {
        //methods
        public static List<Quest> CollectQuests(List<TileV> tiles)
        {
            List<Quest> quests = new List<Quest>();
            foreach (TileV tile in tiles)
            {
                if (tile.Trigger != null)
                {
                    if (tile.Trigger is TriggerQ)
                    {
                        TriggerQ trig = tile.Trigger as TriggerQ;
                        foreach (Quest q in trig.GetQuests())
                        {
                            quests.Add(q);
                        }
                    }
                }
            }
            return quests;
        }
        public static bool HasQuest(Trigger trig, string name)
        {
            if (!(trig is TriggerQ)) return false;
            foreach (Quest q in (trig as TriggerQ).GetQuests())
            {
                if (q.IsCalled(name))
                    return true;
            }
            return false;
        }
        //old methods
        //private bool FindObject(string name)
        //{
        //    foreach (TileV tile in _tiles)
        //    {
        //        if (tile.Object != null)
        //        {
        //            if (tile.Object.IsCalled(name))
        //                return true;
        //        }
        //    }
        //    return false;
        //}
        //private bool FindConvo(string name)
        //{
        //    foreach (TileV tile in _tiles)
        //    {
        //        if (tile.Storybook != null)
        //        {
        //            if (tile.Storybook.IsCalled(name))
        //                return true;
        //        }
        //    }
        //    return false;
        //}
        //private bool FindQuest(string name)
        //{
        //    foreach (TileV tile in _tiles)
        //    {
        //        if (tile.Trigger != null)
        //        {
        //            if (tile.Trigger is TriggerQ)
        //            {
        //                foreach (Quest q in (tile.Trigger as TriggerQ).Quests)
        //                {
        //                    if (q.IsCalled(name))
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}
        //private bool FindArtifact(string name)
        //{
        //    foreach (TileV tile in _tiles)
        //    {
        //        if (tile.Trigger != null)
        //        {
        //            if (tile.Trigger is TriggerA)
        //            {
        //                foreach (Artifact art in (tile.Trigger as TriggerA).Artifacts)
        //                {
        //                    if (art.IsCalled(name))
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}
        //private bool IsKeyQuest(string name)
        //{
        //    if (_wins == null) return true;
        //    return _wins.Contains(name);
        //}
        //private bool CanSolvePuzzle(ActionObject ao)
        //{
        //    ConLine cline = ao.ActionLine as ConLine;
        //    string[] array;
        //    //check that use action of AO can still be done (cleartokens can be cleared)
        //    foreach (ClearToken ct in cline.ClearTokens)
        //    {
        //        if (ct.IsClearedBy(Player)) continue;
        //        array = ct.Name.Split('_');
        //        if (array[0] == "read")
        //        {
        //            if (!Player.Has(array[1], "C"))
        //            {
        //                if (!FindConvo(array[1]))
        //                    return false;
        //            }
        //        }
        //        else if (array[0] == "has")
        //        {
        //            if (!Player.Has(array[1], "A"))
        //            {
        //                if (!FindArtifact(array[1]))
        //                    return false;
        //            }
        //        }
        //        else if (array[0] == "doing" || array[0] == "complete")
        //        {
        //            if (!Player.Has(array[1], "Q") && !Player.Has(array[1], "CQ"))
        //            {
        //                if (!FindQuest(array[1]))
        //                    return false;
        //            }
        //        }
        //        else if (array[0] == "holding")
        //        {
        //            if (Player.Holding != null)
        //            {
        //                if (Player.Holding.IsCalled(array[1]))
        //                    continue;
        //            }
        //            if (!FindObject(array[1]))
        //                return false;
        //        }
        //    }
        //    //check that use action can be done (use artifact avail)
        //    array = cline.Preset.Name.Split('_');
        //    if (!Player.Has(array[2], "A"))
        //    {
        //        if (!FindArtifact(array[2]))
        //            return false;
        //    }
        //    return true;
        //}
        //public void CheckLose()
        //{
        //    bool result = true;
        //    //loop thru all tiles
        //    foreach (TileV tile in _tiles)
        //    {
        //        if (tile.Trigger == null) continue;
        //        if (tile.Trigger is TriggerQ)
        //        {
        //            TriggerQ trig = tile.Trigger as TriggerQ;
        //            foreach (Quest q in trig.GetQuests())
        //            {
        //                if (IsKeyQuest(q.Name))
        //                {
        //                    if (tile.HasPuzzle)
        //                    {
        //                        if (!CanSolvePuzzle(tile.LinkedTo))
        //                        {
        //                            result = false;
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                    }
        //                }
        //            }
        //        }
        //        if (!result) break;
        //    }
        //    //get all requests for an artifact
        //    List<Request> reqs = new List<Request>();
        //    foreach (Quest q in GetQuests())
        //    {
        //        if (IsKeyQuest(q.Name))
        //        {
        //            foreach (Request req in q.ArtifactRequests)
        //            {
        //                reqs.Add(req);
        //            }
        //        }
        //    }
        //    //find in map if artifact still there
        //    foreach (Request req in reqs)
        //    {
        //        if (!FindArtifact(req.GetNeededArtifact))
        //        {
        //            result = false;
        //        }
        //    }
        //    //get all requests that need another quest
        //    reqs = new List<Request>();
        //    foreach (Quest q in GetQuests())
        //    {
        //        if (IsKeyQuest(q.Name))
        //        {
        //            foreach (Request req in q.QuestRequests)
        //            {
        //                reqs.Add(req);
        //            }
        //        }
        //    }
        //    //find in map if quest still avail
        //    foreach (Request req in reqs)
        //    {
        //        if (!FindQuest(req.GetNeededQuest))
        //        {
        //            result = false;
        //        }
        //    }
        //    if (!result)
        //    {
        //        Viewer.Display("\nYou've lost the game, please proceed to reset.");
        //    }
        //    else
        //    {
        //        Viewer.Display("\nYou haven't lost the game yet!");
        //    }
        //}
    }
}
