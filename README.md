# Traceback

### Memory Card Game



A Unity-based memory matching game with save/load functionality and optimized performance.



#### Features



* Card Matching - Flip cards to find pairs with smooth DOTween animations
* Scoring System - Points with combo multipliers and turn tracking
* Save/Load - Resume games with full state persistence
* Game Over - Complete stats display and automatic save cleanup



#### Technical Highlights



* Design Patterns - Singleton, Observer, and State patterns
* Performance - O(1) win detection, component caching, minimal GC
* Architecture - Clean separation: GameManager (logic), CardController (behavior), UIManager (interface)
* Unity Integration - DOTween animations, TextMeshPro, Unity Events



#### Key Optimizations



* Replaced O(n) foreach loop with counter-based win detection
* Cached GetComponent calls to eliminate duplicates
* Event-driven UI updates only when values change
* Proper memory management with object cleanup
