/*
 * Copyright 2017 by Starkku
 * This file is part of MapTool, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */

namespace MapTool
{
    public enum SectionRuleType { Replace, Add, Remove };

    public class ByteIDConversionRule
    {
        public int OriginalStartIndex
        {
            get;
            private set;
        }
        public int OriginalEndIndex
        {
            get;
            private set;
        }
        public int NewStartIndex
        {
            get;
            private set;
        }
        public int NewEndIndex
        {
            get;
            private set;
        }
        public int HeightOverride
        {
            get;
            private set;
        }
        public int SubIndexOverride
        {
            get;
            private set;
        }
        public bool IsRandomizer
        {
            get;
            private set;
        }

        public ByteIDConversionRule(int originalStartIndex, int newStartIndex, int originalEndIndex = -1, int newEndIndex = -1, int heightOverride = -1, int subIndexOverride = -1, bool isRandomizer = false)
        {
            OriginalStartIndex = originalStartIndex;
            if (originalEndIndex < 0) OriginalEndIndex = originalStartIndex;
            else OriginalEndIndex = originalEndIndex;
            NewStartIndex = newStartIndex;
            if (newEndIndex < 0) NewEndIndex = newStartIndex;
            else NewEndIndex = newEndIndex;
            HeightOverride = heightOverride;
            SubIndexOverride = subIndexOverride;
            IsRandomizer = isRandomizer;
        }

        public bool ValidForTiles()
        {
            if (OriginalStartIndex > 65535) return false;
            else if (OriginalEndIndex > 65535) return false;
            else if (NewStartIndex > 65535) return false;
            else if (NewEndIndex > 65535) return false;
            return true;
        }
    }

    public class StringIDConversionRule
    {

        public string Original
        {
            get;
            private set;
        }
        public string New
        {
            get;
            private set;
        }

        public StringIDConversionRule(string original, string replacement)
        {
            Original = original;
            New = replacement;
        }
    }

    public class SectionConversionRule
    {
        public string OriginalSection
        {
            get;
            private set;
        }
        public string NewSection
        {
            get;
            private set;
        }
        public string OriginalKey
        {
            get;
            private set;
        }
        public string NewKey
        {
            get;
            private set;
        }
        public string NewValue
        {
            get;
            private set;
        }
        public SectionConversionRule(string originalSection, string newSection, string originalKey, string newKey, string newValue)
        {
            OriginalSection = originalSection;
            NewSection = newSection;
            OriginalKey = originalKey;
            NewKey = newKey;
            NewValue = newValue;
        }
    }

    public class ModifyOverlaysRule
    {
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        }
        public byte OverlayIndex
        {
            get;
            private set;
        }
        public byte OverlayFrameIndex
        {
            get;
            private set;
        }
        public ModifyOverlaysRule(int x, int y, byte overlayIndex, byte overlayFrameIndex)
        {
			X = x;
			Y = y;
			OverlayIndex = overlayIndex;
			OverlayFrameIndex = overlayFrameIndex;
		}
	}

    public class OverlayConversionRule
    {
        public int OriginalStartIndex
        {
            get;
            private set;
        }
        public int OriginalEndIndex
        {
            get;
            private set;
        }
        public int NewStartIndex
        {
            get;
            private set;
        }
        public int NewEndIndex
        {
            get;
            private set;
        }
        public int OriginalStartIndex2
        {
            get;
            private set;
        }
        public int OriginalEndIndex2
        {
            get;
            private set;
        }
        public int NewStartIndex2
        {
            get;
            private set;
        }
        public int NewEndIndex2
        {
            get;
            private set;
        }
        public int HeightOverride
        {
            get;
            private set;
        }
        public int SubIndexOverride
        {
            get;
            private set;
        }
        public bool IsRandomizer
        {
            get;
            private set;
        }
        public bool IsRandomizer2
        {
            get;
            private set;
        }

        public OverlayConversionRule(int originalStartIndex, int newStartIndex, int originalEndIndex = -1, int newEndIndex = -1, int originalStartIndex2 = -1, int newStartIndex2 = -1, int originalEndIndex2 = -1, int newEndIndex2 = -1, bool isRandomizer = false, bool isRandomizer2 = false)
        {
            OriginalStartIndex = originalStartIndex;
            if (originalEndIndex < 0) OriginalEndIndex = originalStartIndex;
            else OriginalEndIndex = originalEndIndex;
            NewStartIndex = newStartIndex;
            if (newEndIndex < 0) NewEndIndex = newStartIndex;
            else NewEndIndex = newEndIndex;

            OriginalStartIndex2 = originalStartIndex2;
            if (originalEndIndex2 < 0) OriginalEndIndex2 = originalStartIndex2;
            else OriginalEndIndex2 = originalEndIndex2;
            NewStartIndex2 = newStartIndex2;
            if (newEndIndex2 < 0) NewEndIndex2 = newStartIndex2;
            else NewEndIndex2 = newEndIndex2;

			IsRandomizer = isRandomizer;
			IsRandomizer2 = isRandomizer2;
        }

        public bool ValidForOverlays()
        {
            if (OriginalStartIndex > 254) return false;
            else if (OriginalEndIndex > 254) return false;
            else if (NewStartIndex > 255) return false;
            else if (NewEndIndex > 255) return false;
            return true;
        }

        public bool ValidForOverlayFrames()
        {
            if (OriginalStartIndex2 > 255) return false;
            else if (OriginalEndIndex2 > 255) return false;
            else if (NewStartIndex2 > 255) return false;
            else if (NewEndIndex2 > 255) return false;
            return true;
        }
    }
}
