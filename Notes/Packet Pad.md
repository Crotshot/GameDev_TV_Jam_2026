Launches [[packets]] through the air.

"Queen" targeting as options (limit based on type)
Fixed distance (for now)
when hit sends packet out one "side" at a time (rotates clockwise).

Different kinds of packets pads:
 - Y pad, sends packets in a "Y" shape. Only launches in 45 degrees
 - X pad, sends packets in X shape (can be rotated for "+")
 - i pad, sends packets forward
 - Eye pad, send packets forwards and backwards
 - L pad, sends packs at a 90 degree angle
 - Mixing Pad, mixes two different packets together



Design
Just contain a start and an end point.
Keep track of what the previous direction was fired, cycle through
Have a distance(???)
Placeable on [[Grid]]
