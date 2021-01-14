using System.Collections.Generic;using System.Linq;using UnityEngine;using UnityEngine.SceneManagement;using UnityEngine.UI;public class ColorChanger_Jann : Singleton_Jann<ColorChanger_Jann>{public Material m_swapMaterial;public Color m_colorOutGrey60 = new Color(0, 0, 0, 1);public Color m_colorOutGrey122 = new Color(0, 0, 0, 1);public Color m_colorOutGrey174 = new Color(0, 0, 0, 1);public OriginalColor m_particleColor = OriginalColor.Grey60;private List<Image> sb = new List<Image>();private List<Image> sf = new List<Image>();public OriginalColor m_sliderBackgroundColor = OriginalColor.Grey60;public OriginalColor m_sliderFillColor = OriginalColor.Grey122;public OriginalColor m_textColor = OriginalColor.Grey122;public enum OriginalColor{Grey60 = 60,Grey122 = 122,Grey174 = 174}private List<Renderer> rs;private List<Image> imgs;private List<ParticleSystem> pss;private List<Text> txts;private Texture2D cst;public Color[] m_spriteColors;void Awake(){base.Awake();imgs = FOTA<Image>();pss = FOTA<ParticleSystem>();rs = FOTA<Renderer>();txts = FOTA<Text>();GSI("HealthSlider");GSI("HungerSlider");ICC();ACSO();}private void ICC(){Texture2D cST = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);cST.filterMode = FilterMode.Point;for (int i = 0; i < cST.width; ++i)cST.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));cST.Apply();m_swapMaterial.SetTexture("_SwapTex", cST);m_spriteColors = new Color[cST.width];cst = cST;SC((int) OriginalColor.Grey60, m_colorOutGrey60);SC((int) OriginalColor.Grey122, m_colorOutGrey122);SC((int) OriginalColor.Grey174, m_colorOutGrey174);cst.Apply();}public void UpdateColor(SpriteRenderer sr){sr.material = m_swapMaterial;}private void SC(int i, Color c){m_spriteColors[i] = c;cst.SetPixel(i, 0, c);}private void ACSO(){foreach (Renderer rr in rs){rr.material = m_swapMaterial;}foreach (Image img in sb){img.color = m_spriteColors[(int) m_sliderBackgroundColor];}sf.ForEach(i => i.color = m_spriteColors[(int) m_sliderFillColor]);sb.ForEach(i => i.color = m_spriteColors[(int) m_sliderBackgroundColor]);foreach (Image img in imgs){img.material = m_swapMaterial;}foreach (ParticleSystem ps in pss){var m = ps.main;m.startColor = m_spriteColors[(int) m_particleColor];}foreach (Text t in txts){t.color = m_spriteColors[(int) m_textColor];}}private void GSI(string gameObjectName){GameObject s = F(gameObjectName);if (s != null){sb.Add(s.transform.Find("Background").GetComponent<Image>());sf.Add(s.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>());}}private List<T> FOTA<T>(){return SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<T>(true)).ToList();}}