﻿
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Sonic853.Toolkit
{
    public class ImageDisplayer : UdonSharpBehaviour
    {
        public MeshRenderer meshRenderer;
        public Material material;
        public string textureName = "_MainTex";
        public Image image;
        public RawImage rawImage;
        /// <summary>
        /// 图集横排数量
        /// </summary>
        public int atlasWidth = 1;
        /// <summary>
        /// 图集纵排数量
        /// </summary>
        public int atlasHeight = 1;
        /// <summary>
        /// 图集中的第几张图（从 0 开始，从左到右，从上到下）
        /// </summary>
        public int atlasIndex = 0;
        /// <summary>
        /// 根据位置获取图片
        /// </summary>
        public bool usePosition = false;
        /// <summary>
        /// 图片位置 X
        /// </summary>
        public int positionX = 0;
        /// <summary>
        /// 图片位置 Y
        /// </summary>
        public int positionY = 0;
        /// <summary>
        /// 图片位置宽度
        /// </summary>
        public int positionWidth = 2048;
        /// <summary>
        /// 图片位置高度
        /// </summary>
        public int positionHeight = 2048;
        public Texture2D texture;
        public bool SetTexture() => SetTexture(texture);
        public bool SetTexture(Texture2D texture)
        {
            if (meshRenderer != null && meshRenderer.material != null)
            {
                if (string.IsNullOrEmpty(textureName))
                    meshRenderer.material.mainTexture = texture;
                else
                    meshRenderer.material.SetTexture(textureName, texture);
            }
            if (material != null)
            {
                if (string.IsNullOrEmpty(textureName))
                    material.mainTexture = texture;
                else
                    material.SetTexture(textureName, texture);
            }
            if (image != null)
            {
                image.sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f),
                    100,
                    0,
                    SpriteMeshType.FullRect,
                    new Vector4(0, 0, 0, 0),
                    false
                );
            }
            if (rawImage != null)
            {
                rawImage.texture = texture;
            }
            return true;
        }
        public bool SetImage(Image image)
        {
            if (material != null)
            {
                if (string.IsNullOrEmpty(textureName))
                    material.mainTexture = image.sprite.texture;
                else
                    material.SetTexture(textureName, image.sprite.texture);
            }
            if (this.image != null)
            {
                this.image.sprite = image.sprite;
            }
            if (rawImage != null)
            {
                rawImage.texture = image.sprite.texture;
            }
            return true;
        }
        public bool SetSprite(Sprite sprite)
        {
            if (material != null)
            {
                if (string.IsNullOrEmpty(textureName))
                    material.mainTexture = sprite.texture;
                else
                    material.SetTexture(textureName, sprite.texture);
            }
            if (image != null)
            {
                image.sprite = sprite;
            }
            if (rawImage != null)
            {
                rawImage.texture = sprite.texture;
            }
            return true;
        }
    }
}
