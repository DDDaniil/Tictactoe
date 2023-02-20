import {FunctionComponent, useEffect, useState} from "react";
import "./Playground.css";
import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import XPiece from "../../components/Piece/XPiece";
import OPiece from "../../components/Piece/OPiece";
import {cell, player} from "../../constants/types";

interface props {
    connection: HubConnection | undefined,
    setConnection: any
}

const Playground = (props: props) => {
    const [board, setBoard] = useState<cell[]>([0, 0, 0, 0, 0, 0, 0, 0, 0]);
    const [turn, setTurn] = useState(player.X);

    useEffect(() => {
        let newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5291/game', {
                withCredentials: false,
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        props.setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (props.connection) {
            props.connection.start()
                .then(_ => {
                    console.log('Connected!');
                    props.connection?.send("JoinGame", 'First');
                    console.log("joined to " + 'First')
                    onGameChange();
                    onGameEnded();
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [props.connection]);

    const onGameChange = () => {
        props.connection?.on("OnTurn", (b, t) => {
            console.log(b)
            console.log(t)
            setBoard(b);
            setTurn(t);
        });
    }

    const createGame = async () => {
        await props.connection?.send("CreateGame");
    }

    const onGameEnded = () => {
        props.connection?.on("GameEnded", (s, type) => {
            alert(cell[type] + "  win");
        });
    }

    const makeTurn = (id : number) => {
        console.log(id)
        props.connection?.send("MakeTurn", turn+1, id, 'First');
    }

    const check = (id : number) => {
        if (board[id] == cell.Empty) {
            makeTurn(id);
        }
    }

    const choosePiece = (id: number) => {
        if (board[id] == cell.Empty) {
            return turn == player.X ?
                <div style={{opacity: 0.3}}><XPiece/></div> :
                <div style={{opacity: 0.3}}><OPiece/></div>
        }
        return board[id]==cell.X ?
            <XPiece/> :
            <OPiece/>
    }

    const buildBoard = () => {
        return <tbody>
        <tr>
            <td>
                <div onClick={() => check(0)}>
                    {choosePiece(0)}
                </div>
            </td>
            <td>
                <div onClick={() => check(1)}>
                    {choosePiece(1)}
                </div>
            </td>
            <td>
                <div onClick={() => check(2)}>
                    {choosePiece(2)}
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div onClick={() => check(3)}>
                    {choosePiece(3)}
                </div>
            </td>
            <td>
                <div onClick={() => check(4)}>
                    {choosePiece(4)}
                </div>
            </td>
            <td>
                <div onClick={() => check(5)}>
                    {choosePiece(5)}
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div onClick={() => check(6)}>
                    {choosePiece(6)}
                </div>
            </td>
            <td>
                <div onClick={() => check(7)}>
                    {choosePiece(7)}
                </div>
            </td>
            <td>
                <div onClick={() => check(8)}>
                    {choosePiece(8)}
                </div>
            </td>
        </tr>
        </tbody>
    }

    return (
        <div className="page">
            <div>
                <form onSubmit={createGame}>
                    <button type="submit"
                            className="account-button">
                        Create Game
                    </button>
                    <div />
                </form>
            </div>
            <div className="board plate">

                <div className="game">
                    <table>
                        {buildBoard()}
                    </table>
                </div>
            </div>
        </div>
    );
}

export default Playground;