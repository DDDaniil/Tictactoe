import { FunctionComponent } from "react";
import "./StartPage.css";

interface StartPageProps {
}

const StartPage: FunctionComponent<StartPageProps> = () => {
    return (
        <div className="board plate">
            <div className="start">
                <h2>Ничего не успел production</h2>
                <h2>Можно в роуте добавить /game</h2>
            </div>
        </div>
    );
}

export default StartPage;