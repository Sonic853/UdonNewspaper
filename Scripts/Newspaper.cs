
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;
using Sonic853.Toolkit;
using VRC.Udon.Common.Interfaces;
using Sonic853.Udon.UrlLoader;

namespace Sonic853.Newspaper
{
    public class Newspaper : SyncBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] UrlsImageLoader urlsImageLoader;
        [SerializeField] UrlsStringLoader urlsStringLoader;
        [SerializeField] ImageDisplayer[] imageDisplayers;
        [SerializeField] VRCUrl sizeUrl;
        [SerializeField] VRCUrl[] vRCUrls;
        [SerializeField] Texture2D[] textures;
        [SerializeField] Texture2D defaultTexture;
        bool isLoaded = false;
        [UdonSynced] int index;
        public int size;
        [UdonSynced] string currentState = "Default";
        [SerializeField] bool syncObj = false;
        protected override void Start()
        {
            textures = new Texture2D[vRCUrls.Length];
            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = defaultTexture;
            }
            LoadPaper();
            size = 30;
            if (syncObj) base.Start();
        }
        public void LoadPaper() => LoadPaper(false);
        public void ReLoadPaper() => LoadPaper(true);
        public void LoadPaper(bool reload = false)
        {
            urlsStringLoader.PushUrl(sizeUrl, GetComponent<UdonBehaviour>(), nameof(SetPageLoadImage), nameof(SetPage_size), reload);
        }
        [NonSerialized] public string SetPage_size;
        public void SetPageLoadImage() => SetPageLoadImage(false);
        public void SetPageReLoadImage() => SetPageLoadImage(true);
        public void SetPageLoadImage(bool reload = false)
        {
            SetPage(SetPage_size);
            var lastPage = false;
            for (int i = 1; i < size; i++)
            {
                var i1 = i - 1;
                urlsImageLoader.PushUrl(vRCUrls[i1], GetComponent<UdonBehaviour>(), $"LoadImage{i}", nameof(LoadImage_texture), reload);
                if (!lastPage)
                {
                    lastPage = true;
                    urlsImageLoader.PushUrl(vRCUrls[size - 1], GetComponent<UdonBehaviour>(), $"LoadImage{size}", nameof(LoadImage_texture), reload);
                }
            }
        }
        public void SetPage() => SetPage(SetPage_size);
        public void SetPage(string _size)
        {
            int.TryParse(_size, out size);
            if (size % 2 == 1)
                size--;
            isLoaded = true;
        }
        [NonSerialized] public Texture2D LoadImage_texture;
        public void LoadImage1() => LoadImage(0, LoadImage_texture);
        public void LoadImage2() => LoadImage(1, LoadImage_texture);
        public void LoadImage3() => LoadImage(2, LoadImage_texture);
        public void LoadImage4() => LoadImage(3, LoadImage_texture);
        public void LoadImage5() => LoadImage(4, LoadImage_texture);
        public void LoadImage6() => LoadImage(5, LoadImage_texture);
        public void LoadImage7() => LoadImage(6, LoadImage_texture);
        public void LoadImage8() => LoadImage(7, LoadImage_texture);
        public void LoadImage9() => LoadImage(8, LoadImage_texture);
        public void LoadImage10() => LoadImage(9, LoadImage_texture);
        public void LoadImage11() => LoadImage(10, LoadImage_texture);
        public void LoadImage12() => LoadImage(11, LoadImage_texture);
        public void LoadImage13() => LoadImage(12, LoadImage_texture);
        public void LoadImage14() => LoadImage(13, LoadImage_texture);
        public void LoadImage15() => LoadImage(14, LoadImage_texture);
        public void LoadImage16() => LoadImage(15, LoadImage_texture);
        public void LoadImage17() => LoadImage(16, LoadImage_texture);
        public void LoadImage18() => LoadImage(17, LoadImage_texture);
        public void LoadImage19() => LoadImage(18, LoadImage_texture);
        public void LoadImage20() => LoadImage(19, LoadImage_texture);
        public void LoadImage21() => LoadImage(20, LoadImage_texture);
        public void LoadImage22() => LoadImage(21, LoadImage_texture);
        public void LoadImage23() => LoadImage(22, LoadImage_texture);
        public void LoadImage24() => LoadImage(23, LoadImage_texture);
        public void LoadImage25() => LoadImage(24, LoadImage_texture);
        public void LoadImage26() => LoadImage(25, LoadImage_texture);
        public void LoadImage27() => LoadImage(26, LoadImage_texture);
        public void LoadImage28() => LoadImage(27, LoadImage_texture);
        public void LoadImage29() => LoadImage(28, LoadImage_texture);
        public void LoadImage30() => LoadImage(29, LoadImage_texture);
        public void LoadImage(int _index, Texture2D _texture)
        {
            if (_index >= textures.Length) { return; }
            textures[_index] = _texture;
            UpdateImage();
        }
        public void UpdateImage() => UpdateImage(true);
        public void UpdateImage(bool changeAnim)
        {
            imageDisplayers[0].SetTexture(textures[0]);
            imageDisplayers[imageDisplayers.Length - 1].SetTexture(textures[size - 1]);
            if (changeAnim && !animator.GetCurrentAnimatorStateInfo(0).IsName("Open3") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                animator.Play("Open1", 0);
            }
            var _index = index == 0 ? 1 : index;
            imageDisplayers[1].SetTexture(textures[_index]);
            imageDisplayers[2].SetTexture(textures[_index + 1]);
            imageDisplayers[3].SetTexture(textures[_index + 2]);
            imageDisplayers[4].SetTexture(textures[_index + 3]);
        }
        public void NextAll()
        {
            if (syncObj && !isSynced) { return; }
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(NextForce));
        }
        public void Next() => Next(false);
        public void NextForce() => Next(true);
        public void Next(bool force = false)
        {
            if (!isLoaded && !force) { return; }
            if (index + 4 > size)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Open3"))
                {
                    animator.SetTrigger("Open3");
                    UpdateImage(false);
                    currentState = "Open3";
                }
                return;
            }
            if (index == 0)
            {
                index = 1;
                animator.Play("Default", 0);
                animator.SetTrigger("Open1");
                UpdateImage(false);
            }
            else
            {
                index += 2;
                animator.Play("Open1", 0);
                animator.SetTrigger("Open2");
                SendCustomEventDelayedSeconds(nameof(UpdateImage), 0.251f);
            }
            currentState = "Open1";
        }
        public void PreviousAll()
        {
            if (syncObj && !isSynced) { return; }
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(PreviousForce));
        }
        public void Previous() => Previous(false);
        public void PreviousForce() => Previous(true);
        public void Previous(bool force = false)
        {
            if (!isLoaded && !force) { return; }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open3"))
            {
                animator.SetTrigger("Open1");
                currentState = "Open1";
                return;
            }
            if (index == 0) { return; }
            if (index == 1)
            {
                index = 0;
                animator.Play("Open1", 0);
                animator.SetTrigger("Default");
                currentState = "Default";
            }
            else
            {
                index -= 2;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open3"))
                {
                    animator.SetTrigger("Open2");
                    currentState = "Open2";
                }
                else
                {
                    animator.Play("Open2", 0);
                    animator.SetTrigger("Open1");
                    currentState = "Open1";
                }
            }
            UpdateImage(false);
        }
        #region 同步
        public override void OnDeserialization()
        {
            base.OnDeserialization();
            animator.Play(currentState, 0);
            UpdateImage(false);
        }
        #endregion
    }
}
