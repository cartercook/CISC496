// ------------------------------------------------------------------------------
//  _______   _____ ___ ___   _   ___ ___ 
// |_   _\ \ / / _ \ __/ __| /_\ | __| __|
//   | |  \ V /|  _/ _|\__ \/ _ \| _|| _| 
//   |_|   |_| |_| |___|___/_/ \_\_| |___|
// 
// This file has been generated automatically by TypeSafe.
// Any changes to this file may be lost when it is regenerated.
// https://www.stompyrobot.uk/tools/typesafe
// 
// TypeSafe Version: 1.3.2-Unity5
// 
// ------------------------------------------------------------------------------

namespace TypeSafety {
    
    
    public sealed class Layers {
        
        private Layers() {
        }
        
        private const string _tsInternal = "1.3.2-Unity5";
        
        public static global::TypeSafe.Layer Default {
            get {
                return __all[0];
            }
        }
        
        public static global::TypeSafe.Layer TransparentFX {
            get {
                return __all[1];
            }
        }
        
        public static global::TypeSafe.Layer Ignore_Raycast {
            get {
                return __all[2];
            }
        }
        
        public static global::TypeSafe.Layer Water {
            get {
                return __all[3];
            }
        }
        
        public static global::TypeSafe.Layer UI {
            get {
                return __all[4];
            }
        }
        
        public static global::TypeSafe.Layer Obstacle {
            get {
                return __all[5];
            }
        }
        
        public static global::TypeSafe.Layer Unit {
            get {
                return __all[6];
            }
        }
        
        public static global::TypeSafe.Layer AttackCone {
            get {
                return __all[7];
            }
        }
        
        public static global::TypeSafe.Layer VR_UI {
            get {
                return __all[8];
            }
        }
        
        public static global::TypeSafe.Layer Player_Avatar {
            get {
                return __all[9];
            }
        }
        
        public static global::TypeSafe.Layer RTS_UI {
            get {
                return __all[10];
            }
        }
        
        public static global::TypeSafe.Layer Grave {
            get {
                return __all[11];
            }
        }
        
        public static global::TypeSafe.Layer NavMeshAgent {
            get {
                return __all[12];
            }
        }
        
        public static global::TypeSafe.Layer VR_UI_Worldspace {
            get {
                return __all[13];
            }
        }
        
        public static global::TypeSafe.Layer ItemCone {
            get {
                return __all[14];
            }
        }
        
        public static global::TypeSafe.Layer Ground {
            get {
                return __all[15];
            }
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.Layer> __all = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.Layer>(new global::TypeSafe.Layer[] {
                    new global::TypeSafe.Layer("Default", 0),
                    new global::TypeSafe.Layer("TransparentFX", 1),
                    new global::TypeSafe.Layer("Ignore Raycast", 2),
                    new global::TypeSafe.Layer("Water", 4),
                    new global::TypeSafe.Layer("UI", 5),
                    new global::TypeSafe.Layer("Obstacle", 8),
                    new global::TypeSafe.Layer("Unit", 9),
                    new global::TypeSafe.Layer("AttackCone", 10),
                    new global::TypeSafe.Layer("VR UI", 11),
                    new global::TypeSafe.Layer("Player Avatar", 12),
                    new global::TypeSafe.Layer("RTS UI", 13),
                    new global::TypeSafe.Layer("Grave", 14),
                    new global::TypeSafe.Layer("NavMeshAgent", 15),
                    new global::TypeSafe.Layer("VR UI Worldspace", 16),
                    new global::TypeSafe.Layer("ItemCone", 17),
                    new global::TypeSafe.Layer("Ground", 18)});
        
        public static global::System.Collections.Generic.IList<global::TypeSafe.Layer> All {
            get {
                return __all;
            }
        }
    }
}
