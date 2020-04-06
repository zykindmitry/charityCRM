class CustomCheckBoxComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }

    render() {
        return e(
            'label',
            {
                class: this.state.cssClass,
            },
            [
                <InputComponent cssClass="custom-control-input" type="checkbox" />,
                <SpanComponent cssClass="custom-control-label" value={this.state.description} />
            ]
        );
    }
}