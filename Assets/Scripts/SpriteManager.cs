using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : Singleton<SpriteManager>
{
    [SerializeField] List<SpriteAtlas> atlasList;

    Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < atlasList.Count; i++)
        {
            Sprite[] sprites = new Sprite[atlasList[i].spriteCount];
            atlasList[i].GetSprites(sprites);

            for(int j = 0; j < sprites.Length; j++) 
            {
                string spriteName = sprites[j].name.Replace("(Clone)", "").Trim();
                Sprite spriteRef = sprites[j];

                if (!spriteDic.ContainsKey(spriteName))
                {
                    spriteDic.Add(spriteName, sprites[j]);
                }
            }
        }
    }

    public Sprite GetSprite(string _name)
    {
        if(spriteDic.TryGetValue(_name, out Sprite sprite))
        {
            return sprite;
        }

        return null;
    }
}
