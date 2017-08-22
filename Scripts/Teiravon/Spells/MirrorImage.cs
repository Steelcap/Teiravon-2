using System;
using System.Collections;
using Server;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Mobiles;
using Server.Items;

namespace Server.Teiravon
{
    public class MirrorImage
    {
        private static Hashtable m_CloneCount = new Hashtable();

        public static bool HasClone(Mobile m)
        {
            return (m_CloneCount.Contains(m) && ((int)m_CloneCount[m]) > 0);
        }

        public static void AddClone(Mobile m)
        {
            if (m == null)
                return;

            if (m_CloneCount.Contains(m))
                m_CloneCount[m] = ((int)m_CloneCount[m] + 1);
            else
                m_CloneCount.Add(m, 1);
        }

        public static void RemoveClone(Mobile m)
        {
            if (m == null)
                return;

            if (m_CloneCount.Contains(m))
            {
                m_CloneCount[m] = ((int)m_CloneCount[m] - 1);

                if (((int)m_CloneCount[m]) <= 0)
                    m_CloneCount.Remove(m);
            }
        }
    }
}
