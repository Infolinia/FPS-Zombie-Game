using UnityEngine;
using System.Collections;

public class ZarzadcaObrazen : MonoBehaviour {

	public Texture2D[] teksturyObrazenUI;
	public GUITexture wzor;

	Vector3 pozTeksturaLewaDolna;
	Vector3 pozTeksturaLewaGorna;
	Vector3 pozTeksturaPrawaGorna;
	Vector3 pozTeksturaPrawaDolna;

	void Start(){
		pozTeksturaLewaDolna = new Vector3 (0f, 0f, 0f);
		pozTeksturaLewaGorna = new Vector3 (0f, 0.5f, 0f);
		pozTeksturaPrawaGorna = new Vector3 (0.5f, 0.5f, 0f);
		pozTeksturaPrawaDolna = new Vector3 (0.5f, 0f, 0f);

		Rect mojPixelInset = new Rect (0, 0, Screen.width * 0.5f, Screen.height * 0.5f);

		wzor.pixelInset = mojPixelInset;
	}

	public void aktualizujHUD(Transform pozycjaWroga)
	{
		float kat = Vector3.Angle (PozycjaGracza.wektorPrzod, pozycjaWroga.forward);	//obliczam kat miedzy wektorem forward gracza i przeciwnika
		//Debug.Log ("Kąt: " + kat);
		//Debug.Log ("WektorPrzod.z: " + PozycjaGracza.wektorPrzod.z);

		Vector3 pozWzgledna = PozycjaGracza.pozycja - pozycjaWroga.position;			//obliczam wektor wypadkowy, wspolrzedna Z okresla po ktorej stronie przeciwnika jest gracz
		//Debug.Log ("Pozycja wzgledna: " + pozWzgledna);

		if (pozWzgledna.x >= 0) 	//przeciwnik nie jest obrocony w osi Y (sytuacja standardowa)
		{ 	
			if(pozWzgledna.z >= 0)		//gracz z prawej strony przeciwnika 
			{
				//Debug.Log ("Prawa Strona");
				if(PozycjaGracza.wektorPrzod.z <= -0.55f && kat >= 90)
				{					
					stworzObrazeniaUI(teksturyObrazenUI[2], pozTeksturaPrawaGorna);
				}
				else if(PozycjaGracza.wektorPrzod.z <= 0f && kat < 90 && kat >= 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[3], pozTeksturaPrawaDolna);
				}
				else if(PozycjaGracza.wektorPrzod.z > 0f && kat < 90 && kat > 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[0], pozTeksturaLewaDolna);
				}
				else
				{
					stworzObrazeniaUI(teksturyObrazenUI[1], pozTeksturaLewaGorna);
				}
			}
			else 						//gracz z lewej strony
			{
				//Debug.Log ("Lewa Strona");
				if(PozycjaGracza.wektorPrzod.z <= 0.55f && kat >= 90)
				{					
					stworzObrazeniaUI(teksturyObrazenUI[2], pozTeksturaPrawaGorna);
				}
				else if(PozycjaGracza.wektorPrzod.z >= 0 && kat < 90 && kat >= 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[0], pozTeksturaLewaDolna);
				}
				else if(PozycjaGracza.wektorPrzod.z < 0f && kat < 90 && kat > 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[3], pozTeksturaPrawaDolna);
				}
				else
				{
					stworzObrazeniaUI(teksturyObrazenUI[1], pozTeksturaLewaGorna);
				}
			}
		}
		else 							//przeciwnik obrocony w Y sytuacja niestandardowa
		{
			if (pozWzgledna.z >= 0)		//gracz po lewej stronie (odwrotnie niz powyzej mimo iż pozWzgledna.Z ma taka sama wartosc), obracam sie w lewo
			{
				//Debug.Log ("lewa Strona odwrocona");
				if(PozycjaGracza.wektorPrzod.z >= -0.50f && kat >= 90)
				{					
					stworzObrazeniaUI(teksturyObrazenUI[2], pozTeksturaPrawaGorna);
				}
				else if(PozycjaGracza.wektorPrzod.z >= 0 && kat < 90 && kat >= 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[3], pozTeksturaPrawaDolna);
				}
				else if(PozycjaGracza.wektorPrzod.z < 0f && kat < 90 && kat > 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[0], pozTeksturaLewaDolna);
				}
				else
				{
					stworzObrazeniaUI(teksturyObrazenUI[1], pozTeksturaLewaGorna);
				}
			}
			else
			{
				//Debug.Log ("Prawa Strona odwrocona");
				if(PozycjaGracza.wektorPrzod.z >= 0.50f && kat >= 90)
				{					
					stworzObrazeniaUI(teksturyObrazenUI[2], pozTeksturaPrawaGorna);
				}
				else if(PozycjaGracza.wektorPrzod.z >= 0f && kat < 90 && kat >= 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[3], pozTeksturaPrawaDolna);
				}
				else if(PozycjaGracza.wektorPrzod.z < 0f && kat < 90 && kat > 0)
				{
					stworzObrazeniaUI(teksturyObrazenUI[0], pozTeksturaLewaDolna);
				}
				else
				{
					stworzObrazeniaUI(teksturyObrazenUI[1], pozTeksturaLewaGorna);
				}
			}
		}
	}

	void stworzObrazeniaUI(Texture2D tekstura, Vector3 pozycja){
		wzor.texture = tekstura;
		wzor.transform.localPosition = pozycja;
		Instantiate (wzor);
	}

}

