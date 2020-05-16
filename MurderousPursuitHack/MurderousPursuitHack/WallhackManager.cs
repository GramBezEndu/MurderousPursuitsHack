using ProjectX.Camera;
using ProjectX.Characters;
using ProjectX.Levels;
using ProjectX.Player;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MurderousPursuitHack
{
    class WallhackManager : MonoBehaviour
    {
        //PlayerManager playerManager;

        //List<PlayerInfo> playerInfos = new List<PlayerInfo>(); 

        //GUIStyle namesStyle;
        //List<string> debugInfo = new List<string>();

        //GameObject cubeTest;

        //GameObject line;
        //Vector3 center;
        GameInfoManager gameInfoManager;

        public void Start()
        {
            gameInfoManager = FindObjectOfType<GameInfoManager>();
            //gameInfoManager = 
            //playerManager = FindObjectOfType<PlayerManager>();
            //var text = GetComponent<Text>();
            //text.text = "TEST";
            //text.fontSize = 40;
            //text.transform.position = new Vector3(0, 0, 0);

            //namesStyle = new GUIStyle(GUI.skin.label);
            //namesStyle.fontSize = 120;
            //namesStyle.normal.textColor = Color.red;

            //cubeTest = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cubeTest.transform.position = new Vector3(0f, 0.5f, 0f);
            //cubeTest.transform.localScale = new Vector3(1.25f, 1.5f, 1f);
            //var s = SceneManager.GetActiveScene();
            //cubeTest.renderer.material.color = Color.brown;
            //MeshRenderer meshRenderer = cubeTest.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            //if(meshRenderer != null)
            //{
            //    debugInfo.Add("POGCHAMPIK");
            //}
            //var t = cubeTest.GetComponent<MeshRenderer>();
            //if(t != null)
            //{
            //    t.enabled = true;
            //    debugInfo.Add("Found mesh renderer for cube!!!!!!!!!!!!!!!!!");
            //}
            //GameObject localPlayer = new GameObject("LP", typeof(RectTransform));
            //localPlayer.transform.SetParent(gameObject.transform);
            //RectTransform rt = localPlayer.GetComponent<RectTransform>();
            //rt.sizeDelta = new Vector2(150, 50);
            //rt.anchoredPosition = new Vector2(0, 0);
            //localPlayer.AddComponent<Text>().text = "Score = 0";
            //localPlayer.transform.SetParent(playerManager.transform);

            //var lineObject = new GameObject("Line");
            //var x = new LineRenderer();
            //lineRenderer = lineObject.AddComponent(x);

            //line = new GameObject("Line");
            //LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            //lineRenderer.widthMultiplier = 0.2f;
            //lineRenderer.positionCount = 50;

            //// A simple 2 color gradient with a fixed alpha of 1.0f.
            //float alpha = 1.0f;
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            //);
            //lineRenderer.colorGradient = gradient;

            //line.SetWidth(startWidth, endWidth);
            //line.SetVertexCount(3);
            //line.material = aMaterial;
        }

        public void Update()
        {
            //LineRenderer lineRenderer = GetComponent<LineRenderer>();
            //var t = Time.time;
            //for (int i = 0; i < 50; i++)
            //{
            //    lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
            //}
            //if (playerManager == null)
            //{
            //    playerManager = FindObjectOfType<PlayerManager>();
            //}
            //else
            //{
            //    AddDbgMessage("Found player manager");
            //    playerInfos.Clear();
            //    foreach(var p in playerManager.thePlayers.Values)
            //    {
            //        XCharacterMovement xCharacterMovement = (XCharacterMovement)
            //            (typeof(XPlayer).GetField("characterMovement", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance).GetValue(p));
            //        Transform characterTransform = (Transform)(typeof(XCharacterMovement).GetField("characterTransform", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance).GetValue(xCharacterMovement));
            //        //foreach(Transform childTransform in xCharacterMovement.GetComponentInChildren<Transform>())
            //        //{
            //        //    var childRenderer = childTransform.GetComponent<Renderer>();
            //        //    if (childRenderer != null)
            //        //    {
            //        //        AddDbgMessage(childTransform.name);
            //        //        childRenderer.material.shader = Shader.Find("Diffuse");
            //        //        childRenderer.material.color = Color.red;
            //        //        //AddDbgMessage("color: " + t.material.color.ToString());
            //        //    }
            //        //}
            //        //characterTransform
            //        //var renderer = characterTransform.gameObject.GetComponent<Renderer>();
            //        //if (renderer != null)
            //        //    debugInfo.Add("Found renderer!!!!!!!!!");
            //        var collider = xCharacterMovement.GetComponent<Collider>();
            //        if (collider)
            //        {
            //            AddDbgMessage("Collider size: " + collider.bounds.size.ToString());
            //        }
            //        playerInfos.Add(new PlayerInfo()
            //        {
            //            Name = p.name,
            //            IsLocalPlayer = xCharacterMovement.isLocalPlayer,
            //            IsHunterForLocal = false,
            //            IsQuarryForLocal = false,
            //            //Note: use characterTransform.position (not transform.position [maybe the other one will work too, dunno])
            //            Position = /*xCharacterMovement.transform.position,*/characterTransform.position,
            //            Velocity = xCharacterMovement.Velocity,
            //            Size = collider.bounds.size,
            //            CharacterAbilities = xCharacterMovement.Abilities,
            //            Collider = collider,
            //        }) ;
            //    }
            //    //foreach (var p in playerInfos)
            //    //{
            //    //    if (p.IsLocalPlayer)
            //    //    {
            //    //        //cubeTest.transform.parent = p
            //    //        //cubeTest.transform.position = p.Position;
            //    //        //cubeTest.transform.localScale = p.Collider.transform.localScale;
            //    //    }
            //    //}
            //    //var localPlayer = playerManager.GetLocalPlayer();
            //    //if(localPlayer)
            //    //{
            //    //    AddDbgMessage("Found local player");
            //    //    var playerRenderer = localPlayer.GetComponent<Renderer>();
            //    //    if (playerRenderer != null)
            //    //    {
            //    //        AddDbgMessage("Found player renderer");
            //    //        playerRenderer.material.shader = Shader.Find("Diffuse");
            //    //        playerRenderer.material.color = Color.green;
            //    //    }
            //    //    var characterMovement = (XCharacterMovement)(typeof(XPlayer).GetField("characterMovement", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance).GetValue(localPlayer));
            //    //    if (characterMovement)
            //    //    {
            //    //        //characterMovement.transform.
            //    //        AddDbgMessage("Found player -> characterMovement");
            //    //        if (characterMovement.GetComponent<Renderer>() != null)
            //    //        {
            //    //            AddDbgMessage("Found Player -> characterMovement renderer");
            //    //            //characterMovement.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
            //    //            //characterMovement.GetComponent<Renderer>().material.color = Color.yellow;
            //    //            //debugInfo.Add(" + yellow");
            //    //        }
            //    //    }
            //    //}
            //    //foreach(var p in playerManager.thePlayers.Values)
            //    //{
            //    //    var r = p.GetComponent<Renderer>();
            //    //    if(r)
            //    //    {
            //    //        AddDbgMessage("Found renderer!!!");
            //    //    }
            //    //}
            //}

            //var xyz = FindObjectsOfType<XPlayer>();
            //debugInfo = string.Format("Found {0} xPlayer objects\n", xyz.Length);
            //foreach(var ppp in xyz)
            //{
            //    ppp.GetComponent<Renderer>().material.color = Color.red;
            //}

            //if(LevelManager.Instance.CurrentLevel != LevelType.None)
            //{
            //Renderer[] renderers = (Renderer[])UnityEngine.Object.FindObjectsOfType(typeof(Renderer));
            ////debugInfo = string.Format("Found {0}  renderers\n", renderers.Length);
            //foreach (var r in renderers)
            //{
            //    //if (r.gameObject.tag != "Untagged")
            //    //debugInfo.Add(r.gameObject.tag + "\n");

            //    r.material.shader = Shader.Find("Diffuse");
            //    r.material.color = Color.green;

            //    //if (r.gameObject.tag != "Untagged")
            //    //    r.material.color = Color.black;
            //    //r.gameObject.SetActive(false);
            //    //if (r.gameObject.CompareTag("XActor"))
            //    //    r.gameObject.SetActive(false);
            //}
            //}

            //var tttt = (XActor[])UnityEngine.Object.FindObjectsOfType(typeof(XActor));
            ////var tttt = Opsive.ThirdPersonController.Utility.
            ////AddDbgMessage(string.Format("Found {0} XActor objects\n", tttt.Length));
            //foreach (var x in tttt)
            //{
            //    if(x.GetComponent<Renderer>() != null)
            //    {
            //        //AddDbgMessage("Found renderer for XActor\n");
            //        x.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
            //        x.GetComponent<Renderer>().material.color = Color.green;
            //    }
            //    //if (x.GetBodySMRs == null)
            //    //    Application.Quit();
            //    //debugInfo.Add(string.Format("Found bodySMRs collection: {0} \n", x.GetBodySMRs));
            //    //debugInfo.Add(string.Format("bodySMRs count {0}\n", x.GetBodySMRs.Length));
            //    //foreach (var r in x.GetBodySMRs)
            //    //{
            //    //    //debugInfo.Add(r.enabled.ToString());
            //    //    r.material.color = Color.green;
            //    //}
            //}

            //var text = GetComponent<Text>();
            //text.text = "TEST";
            //text.fontSize = 40;
            //text.transform.position = new Vector3(0, 0, 0);
            //playerNames.Add(text);
            //playerNames[0].transform.SetParent(FindObjectOfType<ThirdPersonCamera>().transform);

            //playerNames.Clear();
            //var t = GetComponent<Text>();
            //t.text = playerManager.GetLocalPlayer().name;
            //t.transform.position = new Vector2(0, 0);
            //playerNames.Add(t);

            //foreach (XPlayer player in playerManager.thePlayers.Values)
            //{
            //    texts.Clear();
            //    var t = GetComponent<Text>();
            //    t.text = player.name;
            //    t.transform.position = new Vector2(0, 0);
            //}
        }

        public void OnGUI()
        {
            if (gameInfoManager == null)
                throw new Exception("GameInfoManager not present");
            if (gameInfoManager.IsGameInProgress)
            {
                GUI.color = Color.yellow;

                //GUI.Label(new Rect(0, 0, 400, 30), "Dbg count: " + debugInfo.Count.ToString());

                GUI.Label(new Rect(0, 30, 400, 30), "Current scene: " + SceneManager.GetActiveScene().name);

                //Vector2 currentPos = new Vector2(0, 60);
                //foreach(var dbg in debugInfo)
                //{
                //    GUI.Label(new Rect(currentPos.x, currentPos.y, 400, 20), dbg);
                //    currentPos.y += 20;
                //}

                //var x = playerManager.GetLocalPlayer();
                //var c = (XCharacterMovement)
                //        (typeof(XPlayer).GetField("characterMovement", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance).GetValue(x));
                //GUI.Label(new Rect(1800, 0, 400, 50), c.transform.position.ToString());

                foreach (var p in gameInfoManager.PlayerInfo)
                {
                    if (p.IsLocalPlayer)
                    {
                        GUI.Label(new Rect(1700, 0, 200, 800), p.ToString());
                        //Vector3 min = Camera.main.WorldToScreenPoint(p.Collider.bounds.min);
                        //Vector3 max = Camera.main.WorldToScreenPoint(p.Collider.bounds.max);
                        //Vector3 center = Camera.main.WorldToScreenPoint(p.Collider.bounds.center);

                        //Vector3 boundingPoints = new Vector3[8];
                        //boundingPoints[0] = p.Collider.bounds.min;
                        //boundingPoints[1] = 

                        //Vector3 boundPoint1 = p.Collider.bounds.min;
                        //Vector3 boundPoint2 = p.Collider.bounds.max;
                        //Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
                        //Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
                        //Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
                        //Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
                        //Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
                        //Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

                        //boundPoint1 = Camera.main.WorldToScreenPoint(boundPoint1);
                        //boundPoint2 = Camera.main.WorldToScreenPoint(boundPoint2);
                        //boundPoint3 = Camera.main.WorldToScreenPoint(boundPoint3);
                        //boundPoint4 = Camera.main.WorldToScreenPoint(boundPoint4);
                        //boundPoint5 = Camera.main.WorldToScreenPoint(boundPoint5);
                        //boundPoint6 = Camera.main.WorldToScreenPoint(boundPoint6);
                        //boundPoint7 = Camera.main.WorldToScreenPoint(boundPoint7);
                        //boundPoint8 = Camera.main.WorldToScreenPoint(boundPoint8);

                        //Drawing.DrawLine(new Vector2(boundPoint1.x, Screen.height - boundPoint1.y), new Vector2(boundPoint2.x, Screen.height - boundPoint2.y), Color.blue, 2f);

                        //// top of rectangular cuboid (6-2-8-4)
                        //Drawing.DrawLine(boundPoint6, new Vector2(boundPoint2.x, Screen.height - boundPoint2.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint2, new Vector2(boundPoint8.x, Screen.height - boundPoint8.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint8, new Vector2(boundPoint4.x, Screen.height - boundPoint4.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint4, new Vector2(boundPoint6.x, Screen.height - boundPoint6.y), Color.blue, 4);

                        //// bottom of rectangular cuboid (3-7-5-1)
                        //Drawing.DrawLine(boundPoint3, new Vector2(boundPoint7.x, Screen.height - boundPoint7.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint7, new Vector2(boundPoint5.x, Screen.height - boundPoint5.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint5, new Vector2(boundPoint1.x, Screen.height - boundPoint1.y), Color.blue, 4);
                        //Drawing.DrawLine(boundPoint1, new Vector2(boundPoint3.x, Screen.height - boundPoint3.y), Color.blue, 4);

                        //// legs (6-3, 2-7, 8-5, 4-1)
                        //Drawing.DrawLine(boundPoint6, boundPoint3, Color.blue, 5);
                        //Drawing.DrawLine(boundPoint2, boundPoint7, Color.blue, 5);
                        //Drawing.DrawLine(boundPoint8, boundPoint5, Color.blue, 5);
                        //Drawing.DrawLine(boundPoint4, boundPoint1, Color.blue, 5);
                        //Drawing.DrawLine(new Vector2(boundPoint6.x, Screen.height - boundPoint6.y),
                        //    new Vector2(boundPoint3.x, Screen.height - boundPoint3.y), Color.blue, 4);
                        //Drawing.DrawLine(new Vector2(boundPoint2.x, Screen.height - boundPoint2.y),
                        //    new Vector2(boundPoint7.x, Screen.height - boundPoint7.y), Color.blue, 4);


                        //GUI.color = Color.red;
                        //Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                        //Box(boundPoint1.x, Screen.height - boundPoint1.y, boundPoint1.x - boundPoint2.x, boundPoint1.y - boundPoint2.y, texture);
                        //GUI.color = Color.yellow;

                        //??
                        //cubeTest.transform.position = p.Position;
                        //cubeTest.transform.localScale = p.Collider.transform.localScale;
                        //GUI.
                        //p.Collider.bounds.
                        //GUI.Box(new Rect(min.x, Screen.height - min.y,/*p.Position.x, p.Position.y,*/ 50, 50), "");
                        //GUI.Box(new Rect(min.x, Screen.height - min.y, 100, 50), "");
                        //GUI.Label(new Rect(min.x, Screen.height - min.y, 10, 10), "-----------------------------------------------------------------------------");
                        //Gizmos.DrawCube(center, new Vector3(200f, 200f, 200f));
                    }
                    //Transform x = Camera.main.transform;
                    //x.TransformPoint
                    //ThirdPersonCamera.Instance.Camera.transform.matrix
                    Vector3 toScreenPosition = Camera.main.WorldToScreenPoint/*ThirdPersonCamera.Instance.Camera.WorldToScreenPoint*/(/*new Vector3(p.Position.x, p.Position.z, p.Position.y)*/p.Position);
                    if (toScreenPosition != Vector3.zero)
                    {
                        if (p.IsHunterForLocal)
                            GUI.color = PlayerInfo.Hunter;
                        else if (p.IsQuarryForLocal)
                            GUI.color = PlayerInfo.Quarry;
                        else
                            GUI.color = PlayerInfo.Neutral;
                        GUI.Label(new Rect(toScreenPosition.x, Screen.height - toScreenPosition.y, 200, 50), p.Name);
                        //Bring back old color
                        GUI.color = Color.yellow;
                        //GUI.Box(new Rect())
                    }
                }
            }
        }

        //private void AddDbgMessage(string msg)
        //{
        //    if (!debugInfo.Contains(msg))
        //        debugInfo.Add(msg);
        //}

        //public static void RectFilled(float x, float y, float width, float height, Texture2D text)
        //{
        //    GUI.DrawTexture(new Rect(x, y, width, height), text);
        //}

        //public static void RectOutlined(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        //{
        //    RectFilled(x, y, thickness, height, text);
        //    RectFilled(x + width - thickness, y, thickness, height, text);
        //    RectFilled(x + thickness, y, width - thickness * 2f, thickness, text);
        //    RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, text);
        //}

        //public static void Box(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        //{
        //    RectOutlined(x - width / 2f, y - height, width, height, text, thickness);
        //}
    }
}
