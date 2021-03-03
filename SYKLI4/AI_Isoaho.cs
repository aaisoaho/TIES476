using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Isoaho : PlayerControllerInterface
{
    // T�M� TULEE TEHT�V�SS� T�YDENT��
    // K�yt� vain PlayerControllerInterfacessa olevia metodeja TIMiss� olevan ohjeistuksen mukaan
    
    // Pakota eteen tarkoittaa, ett� teko�ly vaatii itselt��n, ett� seuraava siirto on eteen, ei v�li� mik� on
    private bool pakota_eteen = false;
    /* edellinen liike
     * 0 = ly�
     * 1 = eteen
     * 2 = vasempaan
     * 3 = oikeaan
     */
    private int edellinen   = 0;
    private int seuraava    = 0;

    public override void DecideNextMove()
    {
        // Jos edess� on vihollinen, ly�d��n sit�
        if (GetForwardTileStatus() == 2)
        {
            seuraava = 0;
        } 
        // Jos teko�ly on k�skenyt itse��n liikkumaan eteen ja se on mahdollista
        else if (pakota_eteen && GetForwardTileStatus() == 0)
        {
            seuraava = 1;
        }
        // Muutoin keksit��n muuta t�hteellisemp�� tekemist�
        else
        {
            suunnistaUhriin(etsiUhri());
        }

        // Lopuksi katsotaan tilakoneesta mit� teko�ly tahtoikaan tehd�
        switch(seuraava)
        {
            // 0: Tahdon ly�d�!
            case 0:
                Hit();
                break;
            // Tahdon liikkua eteenp�in!
            case 1:
                pakota_eteen = false;
                MoveForward();
                break;
            // Tahdon k��nty� vasempaan!
            case 2:
                // Kunhan en k��ntynyt �sken oikeaan, niin voin k��nty� vasempaan (Loopin esto)
                if (edellinen != 3)
                    TurnLeft();
                else
                // Koska tahdoin k��nty�, niin k��nnyt��n sitten viel� oikeaan, kun kerta viimeksikin k��nnyin
                    TurnRight();
                break;
                // Muut tarkoittaa, ett� tahdoin k��nty� oikeaan
            default:
                // K��nnyink� vasempaan jo? Jos en, voin k��nty� oikeaan.
                if (edellinen != 2)
                    TurnRight();
                // K��nnyin �sken vasempaan, joten k��nnyn uudestaan.
                else
                    TurnLeft();
                break;

        }
        // Asetetaan edellinen liike ja lasketaan askelia lis��.
        edellinen = seuraava;
    }

    /// <summary>
    /// Funktio, jossa p��tet��n mihin suuntaan yritet��n p��st�.
    /// </summary>
    /// <param name="uhri">Kohteeksi valittu piste.</param>
    private void suunnistaUhriin(Vector2 uhri)
    {
        Vector2 mina = GetPosition();
        
        // Suunnistetaan kohti uhria
        Vector2 suunta = (uhri - mina).normalized;
        float posX = Mathf.Abs(suunta.x);
        float posY = Mathf.Abs(suunta.y);
        // Jos normalisoidun vektorin X on suurempi kuin Y, 
        // kannattaa liikkua Vas.-Oik. suuntiin
        if (posX > posY)
        {
            // Positiivinen: suunnistettava oikealle
            if (suunta.x > 0)
                suunnistaOikealle(uhri);
            // Muutoin liikuttava vas.
            else
                suunnistaVasemmalle(uhri);
        }
        else
        {
            if (suunta.y > 0)
                suunnistaYlos(uhri);
            else
                suunnistaAlas(uhri);
        }

    }
    /// <summary>
    /// Kohde on oikealla p�in. Yritet��n suunnistaa sinne.
    /// </summary>
    /// <param name="uhri">Kohteeksi valittu piste</param>
    private void suunnistaOikealle(Vector2 uhri)
    {
        if (GetRotation().x == 1)
        {
            // Oikea
            if (GetForwardTileStatus() == 0)
            {
                seuraava = 1;
            }
            else
            {
                // Mit� jos sein�?
                pakota_eteen = true;
                kaannyRandomSuuntaan();
            }
        }
        else if (GetRotation().x == -1)
        {
            // Vasen
            kaannyRandomSuuntaan();
        }
        else if (GetRotation().y == 1)
        {
            // Yl�s
            seuraava = 3;
        }
        else
        {
            // Alas
            seuraava = 2;
        }
    }
    private void suunnistaVasemmalle(Vector2 uhri)
    {
        if (GetRotation().x == 1)
        {
            // Oikea
            kaannyRandomSuuntaan();
        }
        else if (GetRotation().x == -1)
        {
            // Vasen
            if (GetForwardTileStatus() == 0)
            {
                seuraava = 1;
            }
            else
            {
                // Mit� jos sein�?
                pakota_eteen = true;
                kaannyRandomSuuntaan();
            }
        }
        else if (GetRotation().y == 1)
        {
            // Yl�s
            seuraava = 2;
        }
        else
        {
            // Alas
            seuraava = 3;
        }
    }

    private void suunnistaYlos(Vector2 uhri)
    {
        if (GetRotation().x == 1)
        {
            // Oikea
            seuraava = 2;
        }
        else if (GetRotation().x == -1)
        {
            // Vasen
            seuraava = 3;
        }
        else
        {
                // Yl�s
                if (GetForwardTileStatus() == 0)
                {
                    seuraava = 1;
                }
                else
                {
                    // Mit� jos sein�?
                    pakota_eteen = true;
                    kaannyRandomSuuntaan();
                }
        }
    }

    private void suunnistaAlas(Vector2 uhri)
    {
        if (GetRotation().x == 1)
        {
            // Oikea
            seuraava = 3;
        }
        else if (GetRotation().x == -1)
        {
            // Vasen
            seuraava = 2;
        }
        else
        {
            // Alas
            if (GetForwardTileStatus() == 0)
            {
                seuraava = 1;
            }
            else
            {
                // Mit� jos sein�?
                pakota_eteen = true;
                kaannyRandomSuuntaan();
            }
        }
    }

    private void kaannyRandomSuuntaan()
    {
        if (Random.value <= 0.5)
        {
            seuraava = 2;
        }
        else
        {
            seuraava = 3;
        }
    }
    /// <summary>
    ///     etsiUhri() etsii matalimmalla el�m�pistetasolla olevista vihollisista l�himm�n ja palauttaa sen sijainnin.
    /// </summary>
    /// <returns>Palauttaa sijainnin, jossa on matalimmalla el�m�pistetasolla oleva vihollinen, joka on l�himp�n�.</returns>
    private Vector2 etsiUhri()
    {
        // Ensin listataan kaikki viholliset, joista etsit��n matalimman el�m�n viholliset
        Vector2[] viholliset = GetEnemyPositions();
        // Botin maksimiel�m�t ovat 3, joten siihen on hyv� verrata
        int matalinElama = 3;
        // K�yd��n kaikki viholliset l�pi
        for (int i = 0; i < viholliset.Length; i++)
        {
            if (GetEnemyHP(viholliset[i]) < matalinElama)
                matalinElama = GetEnemyHP(viholliset[i]);
        }
        // Haetaan vihollisista matalimman el�m�n viholliset
        List<Vector2> potentiaalisetUhrit = new List<Vector2>();
        for (int i = 0; i < viholliset.Length; i++)
        {
            if (GetEnemyHP(viholliset[i]) == matalinElama)
                potentiaalisetUhrit.Add(viholliset[i]);
        }
        // Otetaan talteen oma sijainti, sek� et�isyys ensimm�iseen listallaolijaan
        Vector2 mina = GetPosition();
        double matalinEtaisyys = Vector2.Distance(potentiaalisetUhrit[0], mina);
        Vector2 uhri = potentiaalisetUhrit[0];
        // Etsit��n, onko kukaan l�hemp�n� kuin ensimm�inen.
        potentiaalisetUhrit.ForEach(kohde =>
        {
            // Jos et�isyys on pienempi, niin ollaan l�himp�n� t�h�n asti
            if (Vector2.Distance(kohde,mina) < matalinEtaisyys)
            {
                uhri = kohde;
                matalinEtaisyys = Vector2.Distance(kohde, mina);
            }
        });
        return uhri;
    }

}
