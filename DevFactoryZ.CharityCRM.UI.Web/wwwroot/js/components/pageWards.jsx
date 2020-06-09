import { httpGet } from '../common.js';

const rootWardCategoriesURL = "/api/wardcategories?onlyRoot=true";
const allWardsByCategoryURL = "/api/wards?categoryId=";
const wardsTableHeader = ["№ п/п", "ФИО", "Дата рождения", "Дата регистрации в фонде (UTC)", "Категории"];
const tableColumnsCount = wardsTableHeader.length;

const e = React.createElement;

export class PageWards extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
        this.getWards = this.getWards.bind(this);        
    }

    async getRootWardCategories() {
        await httpGet(rootWardCategoriesURL).then(text => {
            this.setState({
                categories: JSON.parse(text)
            });
        });
    }

    async getWards(categoryId) {
        await httpGet(allWardsByCategoryURL + categoryId).then(text => {
            this.setState({
                wardsForView: this.parseWardListViewModelForTable(text),
                currentCategory: this.findCategoryById(categoryId)
            });
        });
    }

    parseWardListViewModelForTable(source) {
        return JSON.parse(source, function (key, value) {
            if (key.toLowerCase() == 'createdat') {
                return (new Date(Date.parse(value.slice(0, 19)))).toLocaleString();
            }
            if (key.toLowerCase() == 'birthdate') {
                return (new Date(Date.parse(value.slice(0, 19)))).toLocaleDateString();
            }
            if (key.toLowerCase() == 'fullname') {
                return `${value.surName} ${value.firstName} ${value.middleName}`.trim();
            }
            if (key.toLowerCase() == 'wardcategories') {
                return value.map(item => (item.name)).toString();
            }
            return value;
        }).map((item, index) =>
            ([Number(index) + 1,
                item.fullName,
                item.birthDate,
                item.createdAt,
                item.wardCategories]
            )
        );
    }

    findCategoryById(id) {
        let category = this.state.categories.find(item => item.id == id);

        if (category == undefined) {
            category = this.state.categories
                .map(item => item.subCategories)
                .reduce((result, current) => result.concat(current), [])
                .find(subItem => subItem.id == id);
        }

        return category;
    }

    componentDidMount() {
        this.getRootWardCategories();
    }

    componentWillUnmount() {
        this.setState({
            categories: null,
            wardsForView: null,
            currentCategory: null
        });
    }

    render() {
        return e(
            Body,
            {
                rootCategories: this.state.categories,
                getWards: this.getWards,
                wardsForView: this.state.wardsForView,
                currentCategory: this.state.currentCategory
            }
        );
    }
}

function Body(props){
    return (
        <div>
            <PageWardsLeftSideBar formName="Меню" topLevelName="Категории" rootCategories={props.rootCategories} getWards={props.getWards} />
            <PageWardsContent firstLevelName="Категории" currentCategory={props.currentCategory} wardsForView={props.wardsForView}/>
        </div>
    );
};

function PageWardsLeftSideBar(props) {
    return (
        <div className="nav-left-sidebar sidebar-dark">
            <div className="menu-list">
                <div className="navbar navbar-expand-lg navbar-light">
                    <a className="d-xl-none d-lg-none" href="#">
                        {props.formName}
                    </a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav flex-column">
                            <li className="nav-divider d-none d-xl-block d-lg-block">
                                {props.formName}
                            </li>
                            <li className="nav-item">
                                <a className="nav-link active" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1" aria-controls="submenu-1">
                                    <i className="fa fa-fw fa-user-circle"></i>
                                    {props.topLevelName}
                                    <span className="badge badge-success">6</span>
                                </a>
                                <PageWardsCategoryMenu menuItems={props.rootCategories} getWards={props.getWards} />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

function PageWardsContent(props) {
    return (
        <div className="dashboard-wrapper">
            <div className="container-fluid  dashboard-content">
                <div className="row">
                    <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div className="page-header">
                            <h2 className="pageheader-title">Подопечные</h2>
                            <p className="pageheader-text">Proin placerat ante duiullam scelerisque a velit ac porta, fusce sit amet vestibulum mi. Morbi lobortis pulvinar quam.</p>
                            <div className="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol className="breadcrumb">
                                        <li className="breadcrumb-item">
                                            <a href="#" className="breadcrumb-link">
                                                {props.firstLevelName}
                                            </a>
                                        </li>
                                        <li className="breadcrumb-item">
                                            <a href="#" className="breadcrumb-link">
                                                {(props.currentCategory != null && props.currentCategory != undefined && props.currentCategory != '')
                                                    ? props.currentCategory.name
                                                    : ''
                                                }
                                            </a>
                                        </li>
                                        <li className="breadcrumb-item active" aria-current="page">
                                            {(props.currentCategory != null && props.currentCategory != undefined && props.currentCategory != '')
                                                ? 'Подопечные'
                                                : ''
                                            }                                            
                                        </li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div className="card">
                            <div className="card-header" hidden>
                                <h5 className="mb-0">Data Tables - Print, Excel, CSV, PDF Buttons</h5>
                                <p>This example shows DataTables and the Buttons extension being used with the Bootstrap 4 framework providing the styling.</p>
                            </div>
                            <div className="card-body">
                                <div className="table-responsive">
                                    <FormDataTable tableId="wardsTable" columnsCount={tableColumnsCount} tableHeader={wardsTableHeader} tableFooter="" tableData={props.wardsForView} tableClass="table table-striped table-bordered"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>            
        </div>
    );
};

function PageWardsCategoryMenu(props) {
    let subMenu1;

    if (props.menuItems == undefined || props.menuItems == null || props.menuItems == "") {
        subMenu1 = <li key="0" className="nav-item">
            <a className="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1-1" aria-controls="submenu-1-1">
                -
            </a>
        </li>
    }
    else {
        subMenu1 = props.menuItems.map(item => (
            <li key={item.id} className="nav-item">
                <a className="nav-link" href="#" onClick={() => props.getWards(item.id)} data-toggle="collapse" aria-expanded="false" data-target={"#submenu-1-" + item.id} aria-controls={"submenu-1-" + item.id}>
                    {item.name}
                </a>
                <div id={"submenu-1-" + item.id} className="collapse submenu">
                    <ul className="nav flex-column">
                        {item.subCategories.map(subItem => (
                            <li key={subItem.id} className="nav-item">
                                <a className="nav-link" href="#" onClick={() => props.getWards(subItem.id)}>
                                    {subItem.name}
                                </a>
                            </li>
                        ))}
                    </ul>
                </div>
            </li>
        ));
    }

    return (
        <div id="submenu-1" className="collapse submenu">
            <ul className="nav flex-column">
                {subMenu1}
            </ul>
        </div>
    );
};

function FormDataTable(props) {
    let data = <tr><td colSpan={props.columnsCount}> Данные не найдены. </td></tr>;
    let header = <th colSpan={props.columnsCount}></th>;
    let footer = <th colSpan={props.columnsCount}></th>;

    if (props.tableHeader != undefined && props.tableHeader != null && props.tableHeader != "") {
        header = props.tableHeader.map((item, index) => (<th key={index}>{item}</th>));
    }

    if (props.tableFooter != undefined && props.tableFooter != null && props.tableFooter != "") {
        footer = props.tableHeader.map((item, index) => (<th key={index}>{item}</th>));
    }

    if (props.tableData != undefined && props.tableData != null && props.tableData != "") {
        data = props.tableData.map((item, index) => (
            <tr key={index}>
                {item.map((subItem, subIndex) => (
                    <td key={subIndex}>{subItem}</td>                    
                ))}
            </tr>
        ));
    }

    return (
        <table id={props.tableId} className={props.tableClass}>
            <thead>
                <tr>
                    {header}
                </tr>
            </thead>
            <tbody>
                    {data}
            </tbody>
            <tfoot>
                <tr>
                    {footer}
                </tr>
            </tfoot>
        </table>   
    );
};
