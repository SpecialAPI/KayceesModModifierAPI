using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DiskCardGame;

namespace KayceesModModifierAPI
{
    public class AscensionChallengeSelectPageManager : ManagedBehaviour
    {
        public void Initialize(AscensionChallengeScreen screen, AscensionMenuScreenTransition transition = null)
        {
            if (challengeObjectsForPages == null)
            {
                if(screen != null)
                {
                    pageLength = screen.icons.Count;
                    List<GameObject> toSort = screen.icons.ConvertAll((x) => x.gameObject);
                    toSort.Sort((x, x2) => Mathf.RoundToInt((Mathf.Abs(x.transform.position.y - x2.transform.position.y) < 0.1f ? x.transform.position.x - x2.transform.position.x : x2.transform.position.y - x.transform.position.y) * 100));
                    challengeObjectsForPages = new Dictionary<int, List<GameObject>>
                    {
                        { 0, toSort }
                    };
                }
                else
                {
                    List<AscensionIconInteractable> icons = new List<AscensionIconInteractable>(transition.screenInteractables.FindAll((x) => x.GetComponent<AscensionIconInteractable>() != null).ConvertAll((x) =>
                        x.GetComponent<AscensionIconInteractable>()));
                    pageLength = icons.Count;
                    List<GameObject> toSort = icons.ConvertAll((x) => x.gameObject);
                    toSort.Sort((x, x2) => Mathf.RoundToInt((Mathf.Abs(x.transform.position.y - x2.transform.position.y) < 0.1f ? x.transform.position.x - x2.transform.position.x : x2.transform.position.y - x.transform.position.y) * 100));
                    challengeObjectsForPages = new Dictionary<int, List<GameObject>>
                    {
                        { 0, toSort }
                    };
                }
            }
            this.screen = screen;
            if(screen == null)
            {
                this.transition = transition;
            }
        }

        public void AddPage(List<AscensionChallengeInfo> page)
        {
            Initialize(GetComponent<AscensionChallengeScreen>(), GetComponent<AscensionMenuScreenTransition>());
            List<GameObject> obj = new List<GameObject>();
            foreach(GameObject go in challengeObjectsForPages[0])
            {
                if(go != null)
                {
                    GameObject go2 = Instantiate(go);
                    go2.transform.parent = go.transform.parent;
                    AscensionIconInteractable chall = go2.GetComponent<AscensionIconInteractable>();
                    Debug.Log("chall != null: " + (chall != null));
                    Debug.Log("page != null: " + (page != null));
                    AscensionChallengeInfo missing = ScriptableObject.CreateInstance<AscensionChallengeInfo>();
                    missing.name = "MISSING";
                    missing.activatedSprite = null;
                    missing.iconSprite = null;
                    missing.challengeType = AscensionChallenge.None;
                    missing.description = "";
                    missing.title = "";
                    missing.pointValue = 0;
                    chall.challengeInfo = challengeObjectsForPages[0].IndexOf(go) < page.Count ? page[challengeObjectsForPages[0].IndexOf(go)] : missing;
                    go2.SetActive(false);
                    chall.SetEnabled(true);
                    go2.transform.position = go.transform.position;
                    if(screen != null)
                    {
                        screen.icons.Add(chall);
                    }
                    else
                    {
                        Debug.Log("transition != null: " + (transition != null));
                        Debug.Log("transition.screenInteractables != null: " + (transition.screenInteractables != null));
                        transition.screenInteractables.Add(chall);
                    }
                    obj.Add(go2);
                }
            }
            challengeObjectsForPages.Add(challengeObjectsForPages.Count, obj);
        }

        public void NextPage()
        {
            pageIndex++;
            if (pageIndex >= challengeObjectsForPages.Count)
            {
                pageIndex = 0;
            }
            LoadPage(pageIndex);
        }

        public void PreviousPage()
        {
            pageIndex--;
            if (pageIndex < 0)
            {
                pageIndex = challengeObjectsForPages.Count - 1;
            }
            LoadPage(pageIndex);
        }

        public void LoadPage(int page)
        {
            if (challengeObjectsForPages.ContainsKey(page))
            {
                foreach(KeyValuePair<int, List<GameObject>> kvp in challengeObjectsForPages)
                {
                    if (kvp.Key == page)
                    {
                        kvp.Value.ForEach((x) => x.SetActive(true));
                    }
                    else
                    {
                        kvp.Value.ForEach((x) => x.SetActive(false));
                    }
                }
            }
        }

        public int pageIndex;
        public Dictionary<int, List<GameObject>> challengeObjectsForPages;
        public int pageLength;
        public AscensionChallengeScreen screen;
        public AscensionMenuScreenTransition transition;
    }
}
