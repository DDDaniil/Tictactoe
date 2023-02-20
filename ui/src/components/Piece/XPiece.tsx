import { FunctionComponent } from "react";
import "./Piece.css";

interface XPieceProps {
    
}
 
const XPiece: FunctionComponent<XPieceProps> = () => {
    return ( 
        <img
            className="piece"
            src="/xpiece.png"
            onContextMenu={(e) => e.preventDefault()}
            alt="x" />
     );
}
 
export default XPiece;