import ProductList from "./ProductList";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppSelector, useAppDispatch } from "../../app/store/configureStore";
import { setPageNumber, setProductParams } from "./catalogSlice";
import { Grid, Paper } from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import ProductSearch from "./ProductSearch";
import useProducts from "../../app/hooks/useProducts";



const sortOptions = [
    { value: 'name', label: 'Alphabetical' },
    { value: 'priceDesc', label: 'Price - High to low' },
    { value: 'price', label: 'Price - Low to high' }
]



export default function Catalog() {
    const {products, brands, types, filtersLoaded, metaData} = useProducts();
    const {productParams} = useAppSelector(state => state.catalog);
    const dispatch = useAppDispatch();
  


    if (!filtersLoaded) return <LoadingComponent message='Loading products...' />


    

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <Paper sx={{ mb: 2 }}>
                    <ProductSearch />
                </Paper>
                <Paper sx={{ p: 2, mb: 2 }}>
                    <RadioButtonGroup
                        selectedValue={productParams.orderBy}
                        options={sortOptions}
                        onChange={(e) => dispatch(setProductParams({ orderBy: e.target.value }))}
                    />
                </Paper>
               
                <Paper sx={{ p: 2, mb: 2 }}>
                {brands.map(brand => (
                    <CheckboxButtons
                    key = {brand.brandId}
                    item={brand}
                    checked={(productParams.brands) as unknown as string[]}
                    onChange={(checkedItems: string[]) => dispatch(setProductParams({ brands: checkedItems as string[] }))}
                    
                />
                ))}
        </Paper>
                 
                        <Paper sx={{ p: 2 }}>
                        {types.map(type => (
                        <CheckboxButtons
                            key = {type.productTypeId}
                            item={type} 
                            checked={(productParams.types) as unknown as string[]}
                            onChange={(checkedItems: string[]) => dispatch(setProductParams({ types: checkedItems as string[] }))}
                 />
                 ))}
                    </Paper>
                 
                
            </Grid>
            <Grid item xs={9}>
                <ProductList products={products} />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={9} sx={{mb: 2}}>
                {metaData &&
                <AppPagination 
                    metaData={metaData} 
                    onPageChange={(page: number) => dispatch(setPageNumber({pageNumber: page}))} 
                />}
            </Grid>
        </Grid>
    )
}